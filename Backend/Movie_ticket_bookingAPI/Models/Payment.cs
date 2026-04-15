namespace Movie_ticket_bookingAPI.Models;

public class Payment
{
    public int PaymentId { get; set; }
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = "Success"; // Success | Failed | Pending
    public string Method { get; set; } = "Simulated";
    public DateTime PaymentTime { get; set; } = DateTime.UtcNow;

    public Booking Booking { get; set; } = null!;
}
