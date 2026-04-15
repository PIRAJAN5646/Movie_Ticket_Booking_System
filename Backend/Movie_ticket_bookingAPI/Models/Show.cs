namespace Movie_ticket_bookingAPI.Models;

public class Show
{
    public int ShowId { get; set; }
    public int MovieId { get; set; }
    public int TheatreId { get; set; }
    public DateTime ShowTime { get; set; }
    public string Format { get; set; } = "2D";
    public string Language { get; set; } = "English";
    public decimal BasePrice { get; set; }
    public string Availability { get; set; } = "Available"; // Available | Filling Fast | Sold Out

    public Movie Movie { get; set; } = null!;
    public Theatre Theatre { get; set; } = null!;
    public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
}
