using Microsoft.AspNetCore.Mvc;
using Movie_Ticket_Booking_System.Models;
using Movie_Ticket_Booking_System.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ShowsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ShowsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("bymovie/{movieId:int}")]
    public async Task<ActionResult<IEnumerable<Show>>> GetByMovie(int movieId)
    {
        var shows = await _context.Shows
            .Where(s => s.MovieId == movieId)
            .ToListAsync();

        return Ok(shows);
    }

    [HttpGet("bytheatre/{theatreId:int}")]
    public async Task<ActionResult<IEnumerable<Show>>> GetByTheatre(int theatreId)
    {
        var shows = await _context.Shows
            .Where(s => s.TheatreId == theatreId)
            .ToListAsync();

        return Ok(shows);
    }

    [HttpPost]
    public async Task<ActionResult<Show>> Create(Show show)
    {
        await _context.Shows.AddAsync(show);
        await _context.SaveChangesAsync();

        return Ok(show);
    }
}