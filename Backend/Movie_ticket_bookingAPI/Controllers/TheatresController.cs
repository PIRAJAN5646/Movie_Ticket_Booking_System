using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_ticket_bookingAPI.Data;

namespace Movie_ticket_bookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TheatresController : ControllerBase
{
    private readonly AppDbContext _db;
    public TheatresController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var theatres = await _db.Theatres.ToListAsync();
        return Ok(theatres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var theatre = await _db.Theatres.FindAsync(id);
        if (theatre == null) return NotFound();
        return Ok(theatre);
    }
}
