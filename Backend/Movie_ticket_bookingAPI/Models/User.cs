namespace Movie_ticket_bookingAPI.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal WalletBalance { get; set; } = 1000m;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
