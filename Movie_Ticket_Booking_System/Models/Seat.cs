using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movie_Ticket_Booking_System.Models
{
    public class Seat
{
    [Key]
    public int SeatId { get; set; }

    [ForeignKey("Show")]
    public int ShowId { get; set; }

    [Required]
    public string SeatNumber { get; set; }
    [Required]
    public string SeatType { get; set; }
    [Required]
    public bool IsBooked { get; set; }
}
}