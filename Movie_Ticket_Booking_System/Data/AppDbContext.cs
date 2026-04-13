using Microsoft.EntityFrameworkCore;

namespace Movie_Ticket_Booking_System.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Movie> Movies { get; set; }
        public DbSet<Models.Show> Shows { get; set; }
        public DbSet<Models.Seat> Seats { get; set; }
        public DbSet<Models.Booking> Bookings { get; set; }
        public DbSet<Models.Payment> Payments { get; set; }
        public DbSet<Models.Wallet> Wallets { get; set; }
        public DbSet<Models.BookingSeat> BookingSeats { get; set; }
        public DbSet<Models.Theatre> Theatres { get; set; }
    }
}
