using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movie_Ticket_Booking_System.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Show")]
        public int ShowId { get; set; }

        public DateTime BookingTime { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
    }
}