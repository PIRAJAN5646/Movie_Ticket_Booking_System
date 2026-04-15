namespace Movie_ticket_bookingAPI.Models;

public class Seat
{
    public int SeatId { get; set; }
    public int TheatreId { get; set; }
    public string SeatNumber { get; set; } = string.Empty; // e.g. "A1", "B5"
    public string Row { get; set; } = string.Empty;
    public int Col { get; set; }
    public string SeatType { get; set; } = "Standard"; // Standard | Premium

    public Theatre Theatre { get; set; } = null!;
    public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
}
