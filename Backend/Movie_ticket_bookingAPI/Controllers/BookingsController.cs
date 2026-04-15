using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_ticket_bookingAPI.Data;
using Movie_ticket_bookingAPI.DTOs;
using Movie_ticket_bookingAPI.Models;

namespace Movie_ticket_bookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;
    public BookingsController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingRequest req)
    {
        // Validate show exists
        var show = await _db.Shows.Include(s => s.Theatre).Include(s => s.Movie).FirstOrDefaultAsync(s => s.ShowId == req.ShowId);
        if (show == null) return BadRequest(new { message = "Show not found" });

        // Check seats are available
        var alreadyBooked = await _db.BookingSeats
            .Where(bs => bs.ShowId == req.ShowId && req.SeatIds.Contains(bs.SeatId))
            .Join(_db.Bookings.Where(b => b.Status != "Cancelled"), bs => bs.BookingId, b => b.BookingId, (bs, b) => bs)
            .AnyAsync();

        if (alreadyBooked) return Conflict(new { message = "One or more seats are already booked" });

        // Validate user
        var user = await _db.Users.FindAsync(req.UserId);
        // Allow guest booking if user not found (UserId = 0)
        if (req.UserId != 0 && user == null) return BadRequest(new { message = "User not found" });

        var refCode = $"CB{DateTime.UtcNow:yyyyMMdd}{Random.Shared.Next(1000, 9999)}";
        var booking = new Booking
        {
            UserId = req.UserId == 0 ? 1 : req.UserId, // Default to first user for demo
            ShowId = req.ShowId,
            BookingTime = DateTime.UtcNow,
            TotalAmount = req.TotalAmount,
            Status = "Confirmed",
            ReferenceCode = refCode
        };
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        var seats = await _db.Seats.Where(s => req.SeatIds.Contains(s.SeatId)).ToListAsync();
        foreach (var seat in seats)
        {
            var price = seat.SeatType == "Premium" ? show.BasePrice + 150 : show.BasePrice;
            _db.BookingSeats.Add(new BookingSeat
            {
                BookingId = booking.BookingId,
                ShowId = req.ShowId,
                SeatId = seat.SeatId,
                Price = price
            });
        }

        // Create payment record
        _db.Payments.Add(new Payment
        {
            BookingId = booking.BookingId,
            Amount = req.TotalAmount,
            Status = "Success",
            Method = "Simulated",
            PaymentTime = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        // Deduct from user wallet
        decimal newWalletBalance = 0;
        if (req.UserId > 0 && user != null)
        {
            user.WalletBalance = Math.Max(0, user.WalletBalance - req.TotalAmount);
            newWalletBalance = user.WalletBalance;
            await _db.SaveChangesAsync();
        }

        return Ok(new CreateBookingResponse(booking.BookingId, refCode, req.TotalAmount, "Confirmed", newWalletBalance));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await _db.Bookings
            .Include(b => b.Show).ThenInclude(s => s.Movie)
            .Include(b => b.Show).ThenInclude(s => s.Theatre)
            .Include(b => b.BookingSeats).ThenInclude(bs => bs.Seat)
            .FirstOrDefaultAsync(b => b.BookingId == id);

        if (booking == null) return NotFound();

        return Ok(new BookingDetailDto
        {
            BookingId = booking.BookingId,
            ReferenceCode = booking.ReferenceCode,
            TotalAmount = booking.TotalAmount,
            Status = booking.Status,
            BookingTime = booking.BookingTime,
            MovieTitle = booking.Show.Movie.Title,
            MoviePosterUrl = booking.Show.Movie.PosterUrl,
            TheatreName = booking.Show.Theatre.Name,
            ShowTime = booking.Show.ShowTime,
            Format = booking.Show.Format,
            Language = booking.Show.Language,
            Seats = booking.BookingSeats.Select(bs => bs.Seat.SeatNumber).ToList()
        });
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var bookings = await _db.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Show).ThenInclude(s => s.Movie)
            .Include(b => b.Show).ThenInclude(s => s.Theatre)
            .Include(b => b.BookingSeats).ThenInclude(bs => bs.Seat)
            .OrderByDescending(b => b.BookingTime)
            .Select(b => new BookingDetailDto
            {
                BookingId = b.BookingId,
                ReferenceCode = b.ReferenceCode,
                TotalAmount = b.TotalAmount,
                Status = b.Status,
                BookingTime = b.BookingTime,
                MovieTitle = b.Show.Movie.Title,
                MoviePosterUrl = b.Show.Movie.PosterUrl,
                TheatreName = b.Show.Theatre.Name,
                ShowTime = b.Show.ShowTime,
                Format = b.Show.Format,
                Language = b.Show.Language,
                Seats = b.BookingSeats.Select(bs => bs.Seat.SeatNumber).ToList()
            })
            .ToListAsync();
        return Ok(bookings);
    }
}
