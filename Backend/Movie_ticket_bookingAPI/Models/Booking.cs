namespace Movie_ticket_bookingAPI.Models;

public class Booking
{
    public int BookingId { get; set; }
    public int UserId { get; set; }
    public int ShowId { get; set; }
    public DateTime BookingTime { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Confirmed"; // Confirmed | Cancelled | Pending
    public string ReferenceCode { get; set; } = string.Empty;

    public User User { get; set; } = null!;
    public Show Show { get; set; } = null!;
    public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
}
