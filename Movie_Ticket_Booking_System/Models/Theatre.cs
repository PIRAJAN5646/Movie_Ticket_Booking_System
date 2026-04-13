using System.ComponentModel.DataAnnotations;
namespace Movie_Ticket_Booking_System.Models
{
    public class Theatre
    {
        [Key]
        public int TheatreId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
