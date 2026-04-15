using Microsoft.AspNetCore.Mvc;
using Movie_ticket_bookingAPI.DTOs;
using Movie_ticket_bookingAPI.Services;

namespace Movie_ticket_bookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    public AuthController(AuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var result = await _auth.LoginAsync(req);
        if (result == null) return Unauthorized(new { message = "Invalid email or password" });
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var result = await _auth.RegisterAsync(req);
        if (result == null) return Conflict(new { message = "Email already registered" });
        return Ok(result);
    }
}
