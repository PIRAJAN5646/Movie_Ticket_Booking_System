using Microsoft.AspNetCore.Mvc;
using Movie_Ticket_Booking_System.Models;
using Movie_Ticket_Booking_System.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SeatsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SeatsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("layout/{showId:int}")]
    public async Task<ActionResult<IEnumerable<Seat>>> GetLayoutByShow(int showId)
    {
        var seats = await _context.Seats
            .Where(s => s.ShowId == showId)
            .ToListAsync();

        return Ok(seats);
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateSeatStatus(int id, [FromQuery] bool status)
    {
        var seat = await _context.Seats.FindAsync(id);

        if (seat == null)
            return NotFound();

        seat.IsBooked = status;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}