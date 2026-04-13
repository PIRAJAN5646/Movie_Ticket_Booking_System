using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movie_Ticket_Booking_System.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}