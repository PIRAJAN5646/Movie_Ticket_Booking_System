using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Movie_ticket_bookingAPI.Data;
using Movie_ticket_bookingAPI.DTOs;
using Movie_ticket_bookingAPI.Models;

namespace Movie_ticket_bookingAPI.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest req)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == req.Email);
        if (user == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash)) return null;
        var token = GenerateToken(user);
        return new AuthResponse(token, user.UserId, user.Name, user.Email, user.WalletBalance);
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest req)
    {
        if (_db.Users.Any(u => u.Email == req.Email)) return null;
        var user = new User
        {
            Name = req.Name,
            Email = req.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            Phone = req.Phone,
            WalletBalance = 1000m
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        var token = GenerateToken(user);
        return new AuthResponse(token, user.UserId, user.Name, user.Email, user.WalletBalance);
    }

    private string GenerateToken(User user)
    {
        var jwtKey = _config["Jwt:Key"] ?? "CineBookSuperSecretKeyForJWT2024!!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"] ?? "CineBook",
            audience: _config["Jwt:Audience"] ?? "CineBookUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
