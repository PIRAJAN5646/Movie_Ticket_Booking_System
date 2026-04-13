using Microsoft.AspNetCore.Mvc;
using Movie_Ticket_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using Movie_Ticket_Booking_System.Data;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingsController(AppDbContext context)
    {
        _context = context;
    }

    // DTO for request
    public class BookingRequest
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public List<int> SeatIds { get; set; }
        public decimal TotalAmount { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(BookingRequest request)
    {
        if (request.SeatIds == null || !request.SeatIds.Any())
            return BadRequest("No seats selected");

        var seats = await _context.Seats
            .Where(s => request.SeatIds.Contains(s.SeatId))
            .ToListAsync();

        if (seats.Any(s => s.IsBooked))
            return BadRequest("One or more seats already booked");


        var booking = new Booking
        {
            UserId = request.UserId,
            ShowId = request.ShowId,
            BookingTime = DateTime.UtcNow,
            TotalAmount = request.TotalAmount
        };

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();

        foreach (var seatId in request.SeatIds)
        {
            var bookingSeat = new BookingSeat
            {
                BookingId = booking.BookingId,
                SeatId = seatId
            };

            await _context.BookingSeats.AddAsync(bookingSeat);
        }

        foreach (var seat in seats)
        {
            seat.IsBooked = true;
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Booking successful",
            bookingId = booking.BookingId
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            return NotFound();

        var seatIds = await _context.BookingSeats
            .Where(bs => bs.BookingId == id)
            .Select(bs => bs.SeatId)
            .ToListAsync();

        return Ok(new
        {
            booking,
            seats = seatIds
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            return NotFound();

        var bookingSeats = await _context.BookingSeats
            .Where(bs => bs.BookingId == id)
            .ToListAsync();

        foreach (var bs in bookingSeats)
        {
            var seat = await _context.Seats.FindAsync(bs.SeatId);
            if (seat != null)
                seat.IsBooked = false;
        }

        _context.BookingSeats.RemoveRange(bookingSeats);
        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}