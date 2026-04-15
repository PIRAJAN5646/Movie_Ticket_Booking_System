using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_ticket_bookingAPI.Data;
using Movie_ticket_bookingAPI.DTOs;

namespace Movie_ticket_bookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ShowsController(AppDbContext db) => _db = db;

    // GET api/shows/bymovie/{movieId}?date=2025-05-15&language=English&format=IMAX 2D
    [HttpGet("bymovie/{movieId}")]
    public async Task<IActionResult> GetByMovie(int movieId, [FromQuery] string? date, [FromQuery] string? language, [FromQuery] string? format)
    {
        var query = _db.Shows
            .Include(s => s.Theatre)
            .Where(s => s.MovieId == movieId);

        if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var parsedDate))
            query = query.Where(s => s.ShowTime.Date == parsedDate.Date);
        else
            query = query.Where(s => s.ShowTime.Date == DateTime.UtcNow.Date);

        if (!string.IsNullOrWhiteSpace(language))
            query = query.Where(s => s.Language == language);
        if (!string.IsNullOrWhiteSpace(format))
            query = query.Where(s => s.Format == format);

        var shows = await query.OrderBy(s => s.TheatreId).ThenBy(s => s.ShowTime).ToListAsync();

        // Group by theatre
        var grouped = shows.GroupBy(s => s.TheatreId).Select(g =>
        {
            var theatre = g.First().Theatre;
            return new
            {
                TheatreId = g.Key,
                TheatreName = theatre.Name,
                Address = theatre.Address,
                Distance = theatre.Distance,
                Amenities = theatre.Amenities.Split(',').ToList(),
                CancellationPolicy = theatre.CancellationPolicy,
                Shows = g.Select(s => new
                {
                    s.ShowId,
                    s.MovieId,
                    s.TheatreId,
                    s.ShowTime,
                    s.Format,
                    s.Language,
                    s.BasePrice,
                    s.Availability
                }).ToList()
            };
        }).ToList();

        return Ok(grouped);
    }

    // GET api/shows/{showId}/seats
    [HttpGet("{showId}/seats")]
    public async Task<IActionResult> GetShowWithSeats(int showId)
    {
        var show = await _db.Shows
            .Include(s => s.Theatre)
            .Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.ShowId == showId);

        if (show == null) return NotFound();

        // Get all seats for this theatre
        var seats = await _db.Seats
            .Where(s => s.TheatreId == show.TheatreId)
            .ToListAsync();

        // Get booked seat IDs for this show
        var bookedSeatIds = await _db.BookingSeats
            .Where(bs => bs.ShowId == showId && _db.Bookings.Any(b => b.BookingId == bs.BookingId && b.Status != "Cancelled"))
            .Select(bs => bs.SeatId)
            .ToListAsync();
        var bookedSeatIdsSet = bookedSeatIds.ToHashSet();

        var seatDtos = seats.Select(s => new SeatDto
        {
            SeatId = s.SeatId,
            SeatNumber = s.SeatNumber,
            Row = s.Row,
            Col = s.Col,
            SeatType = s.SeatType,
            IsBooked = bookedSeatIdsSet.Contains(s.SeatId),
            Price = s.SeatType == "Premium" ? show.BasePrice + 150 : show.BasePrice
        }).ToList();

        return Ok(new ShowWithSeatsDto
        {
            ShowId = show.ShowId,
            MovieId = show.MovieId,
            TheatreId = show.TheatreId,
            TheatreName = show.Theatre.Name,
            ShowTime = show.ShowTime,
            Format = show.Format,
            Language = show.Language,
            BasePrice = show.BasePrice,
            Availability = show.Availability,
            Seats = seatDtos
        });
    }
}
