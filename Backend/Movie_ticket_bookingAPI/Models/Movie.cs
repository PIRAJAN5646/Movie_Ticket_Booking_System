namespace Movie_ticket_bookingAPI.Models;

public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int Duration { get; set; } // in minutes
    public string Description { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public string BackdropUrl { get; set; } = string.Empty;
    public string Rating { get; set; } = "8.0";
    public string Votes { get; set; } = "10K";
    public string PgRating { get; set; } = "UA";
    public string Badge { get; set; } = string.Empty;
    public string Formats { get; set; } = "2D,IMAX 2D"; // comma-separated
    public string Languages { get; set; } = "English,Hindi"; // comma-separated
    public string Cast { get; set; } = "[]"; // JSON array
    public string Crew { get; set; } = "{}"; // JSON object

    public ICollection<Show> Shows { get; set; } = new List<Show>();
}
