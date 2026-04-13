using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movie_Ticket_Booking_System.Models
{
    public class Show
    {
        [Key]
        public int ShowId { get; set; }
        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        [Required]
        [ForeignKey("Theatre")]
        public int TheatreId { get; set; }
        [Required]
        public DateTime ShowTime { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
