using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_ticket_bookingAPI.Data;
using Movie_ticket_bookingAPI.Models;

namespace Movie_ticket_bookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _db;

    public MoviesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] string? genre)
    {
        var query = _db.Movies.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(m => m.Title.Contains(search) || m.Genre.Contains(search));
        if (!string.IsNullOrWhiteSpace(genre))
            query = query.Where(m => m.Genre.Contains(genre));

        var movies = await query.OrderByDescending(m => m.Rating).ToListAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _db.Movies.FindAsync(id);
        if (movie == null) return NotFound(new { message = "Movie not found" });
        return Ok(movie);
    }

    [HttpGet("new-arrivals")]
    public async Task<IActionResult> GetNewArrivals()
    {
        var movies = await _db.Movies
            .Where(m => m.ReleaseDate >= DateTime.UtcNow.AddMonths(-3))
            .OrderByDescending(m => m.ReleaseDate)
            .Take(4)
            .ToListAsync();
        return Ok(movies);
    }
}
