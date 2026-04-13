using Microsoft.AspNetCore.Mvc;
using Movie_Ticket_Booking_System.Models;
using Movie_Ticket_Booking_System.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TheatresController : ControllerBase
{
    private readonly AppDbContext _context;

    public TheatresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Theatre>>> GetAll()
    {
        return Ok(await _context.Theatres.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Theatre>> Create(Theatre theatre)
    {
        await _context.Theatres.AddAsync(theatre);
        await _context.SaveChangesAsync();

        return Ok(theatre);
    }
}