namespace Movie_ticket_bookingAPI.Models;

public class Theatre
{
    public int TheatreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Distance { get; set; } = string.Empty;
    public string Amenities { get; set; } = string.Empty; // comma-separated
    public string CancellationPolicy { get; set; } = string.Empty;

    public ICollection<Show> Shows { get; set; } = new List<Show>();
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
