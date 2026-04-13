using System.ComponentModel.DataAnnotations;

namespace Movie_Ticket_Booking_System.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string PosterUrl { get; set; }
        [Required]
        public string ImgUrl { get; set; }
    }
}
