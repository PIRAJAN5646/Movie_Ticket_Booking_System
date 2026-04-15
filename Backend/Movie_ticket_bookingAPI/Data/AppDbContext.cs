using Microsoft.EntityFrameworkCore;
using Movie_ticket_bookingAPI.Models;

namespace Movie_ticket_bookingAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Theatre> Theatres => Set<Theatre>();
    public DbSet<Show> Shows => Set<Show>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingSeat> BookingSeats => Set<BookingSeat>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                MovieId = 1,
                Title = "The Celestial Odyssey",
                Genre = "Sci-Fi · Action · Drama",
                Duration = 169,
                Description = "In a future where Earth is becoming uninhabitable, a team of ex-pilots and scientists travel through a wormhole in search of a new home for humanity. This remastered cinematic journey explores the depths of space, time dilation, and the unbreakable bond of a father's love through dimensions unknown.",
                Language = "English,Hindi,Tamil,Telugu",
                ReleaseDate = new DateTime(2024, 10, 24),
                PosterUrl = "https://picsum.photos/seed/celestial/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1534796636912-3b95b3ab5986?w=1400&q=80",
                Rating = "9.2",
                Votes = "430.2K",
                PgRating = "UA",
                Badge = "PREMIUM 70MM",
                Formats = "2D,IMAX 2D,4DX",
                Languages = "English,Hindi,Tamil,Telugu",
                Cast = "[{\"name\":\"Matthew Cooper\",\"role\":\"Cooper\",\"img\":\"https://picsum.photos/seed/cast1/80/80\"},{\"name\":\"Anne Hathaway\",\"role\":\"Brand\",\"img\":\"https://picsum.photos/seed/cast2/80/80\"},{\"name\":\"Jessica Chastain\",\"role\":\"Murph\",\"img\":\"https://picsum.photos/seed/cast3/80/80\"},{\"name\":\"Michael Caine\",\"role\":\"Professor Brand\",\"img\":\"https://picsum.photos/seed/cast4/80/80\"},{\"name\":\"Matt Damon\",\"role\":\"Mann\",\"img\":\"https://picsum.photos/seed/cast5/80/80\"}]",
                Crew = "{\"director\":\"Christopher Nolan\",\"producer\":\"Emma Thomas\",\"music\":\"Hans Zimmer\",\"writer\":\"Jonathan Nolan\"}"
            },
            new Movie
            {
                MovieId = 2,
                Title = "Avengers: Doomsday",
                Genre = "Action · Adventure",
                Duration = 148,
                Description = "Earth's mightiest heroes unite one last time to face a threat beyond the stars. An epic conclusion to a journey spanning decades.",
                Language = "English,Hindi,Tamil,Telugu",
                ReleaseDate = new DateTime(2025, 5, 1),
                PosterUrl = "https://picsum.photos/seed/avengers/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1518834107812-67b0b7c58434?w=1400&q=80",
                Rating = "9.1",
                Votes = "128K",
                PgRating = "UA",
                Badge = "BLOCKBUSTER",
                Formats = "2D,IMAX 2D,4DX",
                Languages = "English,Hindi,Tamil,Telugu",
                Cast = "[{\"name\":\"Robert Downey Jr.\",\"role\":\"Iron Man\",\"img\":\"https://picsum.photos/seed/rdj/80/80\"},{\"name\":\"Chris Evans\",\"role\":\"Captain America\",\"img\":\"https://picsum.photos/seed/ce/80/80\"},{\"name\":\"Scarlett Johansson\",\"role\":\"Black Widow\",\"img\":\"https://picsum.photos/seed/sj/80/80\"}]",
                Crew = "{\"director\":\"Anthony Russo\",\"producer\":\"Kevin Feige\",\"music\":\"Alan Silvestri\",\"writer\":\"Christopher Markus\"}"
            },
            new Movie
            {
                MovieId = 3,
                Title = "Neon Nights",
                Genre = "Sci-Fi · Thriller",
                Duration = 132,
                Description = "A cyberpunk detective story set in a rain-drenched future city where the line between human and AI has completely blurred.",
                Language = "English,Hindi",
                ReleaseDate = new DateTime(2025, 3, 14),
                PosterUrl = "https://picsum.photos/seed/neon/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1545569341-9eb8b30979d9?w=1400&q=80",
                Rating = "8.6",
                Votes = "74K",
                PgRating = "A",
                Badge = "",
                Formats = "2D,IMAX 2D",
                Languages = "English,Hindi",
                Cast = "[{\"name\":\"Ryan Gosling\",\"role\":\"Detective Cole\",\"img\":\"https://picsum.photos/seed/rg/80/80\"},{\"name\":\"Zendaya\",\"role\":\"Nova\",\"img\":\"https://picsum.photos/seed/zend/80/80\"}]",
                Crew = "{\"director\":\"Denis Villeneuve\",\"producer\":\"Andrew Rona\",\"music\":\"Hans Zimmer\",\"writer\":\"Eric Roth\"}"
            },
            new Movie
            {
                MovieId = 4,
                Title = "Whispering Ghosts",
                Genre = "Horror · Mystery",
                Duration = 118,
                Description = "A family moves into an ancestral mansion only to discover dark secrets buried within its walls. Terror has never been this personal.",
                Language = "English,Hindi,Tamil",
                ReleaseDate = new DateTime(2025, 10, 31),
                PosterUrl = "https://picsum.photos/seed/ghost/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1509248961158-e54f6934749c?w=1400&q=80",
                Rating = "8.2",
                Votes = "52K",
                PgRating = "A",
                Badge = "",
                Formats = "2D",
                Languages = "English,Hindi,Tamil",
                Cast = "[{\"name\":\"Florence Pugh\",\"role\":\"Dr. Harper\",\"img\":\"https://picsum.photos/seed/fp/80/80\"},{\"name\":\"Oscar Isaac\",\"role\":\"Elias\",\"img\":\"https://picsum.photos/seed/oi/80/80\"}]",
                Crew = "{\"director\":\"James Wan\",\"producer\":\"Peter Safran\",\"music\":\"Joseph Bishara\",\"writer\":\"Leigh Whannell\"}"
            },
            new Movie
            {
                MovieId = 5,
                Title = "The Last Laugh",
                Genre = "Comedy · Drama",
                Duration = 105,
                Description = "A washed-up comedian gets one last shot at redemption in the most unexpected of places after a series of life-altering events.",
                Language = "English,Hindi",
                ReleaseDate = new DateTime(2025, 4, 5),
                PosterUrl = "https://picsum.photos/seed/comedy/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1485846234645-a62644f84728?w=1400&q=80",
                Rating = "7.9",
                Votes = "41K",
                PgRating = "U",
                Badge = "",
                Formats = "2D",
                Languages = "English,Hindi",
                Cast = "[{\"name\":\"Adam Sandler\",\"role\":\"Max\",\"img\":\"https://picsum.photos/seed/as/80/80\"},{\"name\":\"Jennifer Aniston\",\"role\":\"Lisa\",\"img\":\"https://picsum.photos/seed/ja/80/80\"}]",
                Crew = "{\"director\":\"Judd Apatow\",\"producer\":\"Barry Mendel\",\"music\":\"Christophe Beck\",\"writer\":\"Judd Apatow\"}"
            },
            new Movie
            {
                MovieId = 6,
                Title = "Quantum Tide",
                Genre = "Action · Sci-Fi",
                Duration = 142,
                Description = "When quantum physics meets action, reality shatters. A physicist and a special agent race against time to save multiple timelines.",
                Language = "English,Hindi,Tamil,Telugu",
                ReleaseDate = new DateTime(2025, 6, 20),
                PosterUrl = "https://picsum.photos/seed/quantum/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1451187580459-43490279c0fa?w=1400&q=80",
                Rating = "8.8",
                Votes = "89K",
                PgRating = "UA",
                Badge = "MUST WATCH",
                Formats = "2D,IMAX 2D,4DX",
                Languages = "English,Hindi,Tamil,Telugu",
                Cast = "[{\"name\":\"Tom Holland\",\"role\":\"Dr. Parker\",\"img\":\"https://picsum.photos/seed/th/80/80\"},{\"name\":\"Anya Taylor-Joy\",\"role\":\"Agent Kira\",\"img\":\"https://picsum.photos/seed/atj/80/80\"}]",
                Crew = "{\"director\":\"J.J. Abrams\",\"producer\":\"David Heyman\",\"music\":\"John Williams\",\"writer\":\"Chris Terrio\"}"
            },
            new Movie
            {
                MovieId = 7,
                Title = "Velocity X2",
                Genre = "Action · Drama",
                Duration = 126,
                Description = "The sequel to the global hit. High-octane races, dangerous rivalries, and a family torn apart by betrayal.",
                Language = "English,Hindi,Tamil",
                ReleaseDate = new DateTime(2025, 7, 15),
                PosterUrl = "https://picsum.photos/seed/velocity/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1568605117036-5fe5e7bab0b7?w=1400&q=80",
                Rating = "8.3",
                Votes = "65K",
                PgRating = "UA",
                Badge = "",
                Formats = "2D,IMAX 2D",
                Languages = "English,Hindi,Tamil",
                Cast = "[{\"name\":\"Vin Diesel\",\"role\":\"Marco\",\"img\":\"https://picsum.photos/seed/vd/80/80\"},{\"name\":\"Michelle Rodriguez\",\"role\":\"Leticia\",\"img\":\"https://picsum.photos/seed/mr/80/80\"}]",
                Crew = "{\"director\":\"Justin Lin\",\"producer\":\"Neal H. Moritz\",\"music\":\"Brian Tyler\",\"writer\":\"Gary Scott Thompson\"}"
            },
            new Movie
            {
                MovieId = 8,
                Title = "Kingdom Fallen",
                Genre = "Fantasy · Adventure",
                Duration = 157,
                Description = "An ancient kingdom rises again. The chosen warrior must unite fractured realms before the shadow swallows the world whole.",
                Language = "English,Hindi,Tamil,Telugu",
                ReleaseDate = new DateTime(2025, 12, 19),
                PosterUrl = "https://picsum.photos/seed/kingdom/300/450",
                BackdropUrl = "https://images.unsplash.com/photo-1518709268805-4e9042af9f23?w=1400&q=80",
                Rating = "8.7",
                Votes = "93K",
                PgRating = "UA",
                Badge = "EPIC",
                Formats = "2D,IMAX 2D,4DX",
                Languages = "English,Hindi,Tamil,Telugu",
                Cast = "[{\"name\":\"Henry Cavill\",\"role\":\"Aldric\",\"img\":\"https://picsum.photos/seed/hc/80/80\"},{\"name\":\"Gal Gadot\",\"role\":\"Queen Lyra\",\"img\":\"https://picsum.photos/seed/gg/80/80\"}]",
                Crew = "{\"director\":\"Ridley Scott\",\"producer\":\"Ridley Scott\",\"music\":\"Harry Gregson-Williams\",\"writer\":\"William Nicholson\"}"
            }
        );

        // Seed Theatres
        modelBuilder.Entity<Theatre>().HasData(
            new Theatre
            {
                TheatreId = 1,
                Name = "PVR: ICON Gold, Phoenix Market City",
                Location = "Kurla West, Mumbai",
                Address = "LBS Marg, Kurla West, Mumbai, Maharashtra 400070",
                Distance = "2.4 km",
                Amenities = "M Ticket,Food & Beverage,Cancellation",
                CancellationPolicy = "MINI CANCELLATION · Available until 20 min before show"
            },
            new Theatre
            {
                TheatreId = 2,
                Name = "INOX: R-City, Ghatkopar",
                Location = "Ghatkopar West, Mumbai",
                Address = "R City Mall, LBS Marg, Ghatkopar West, Mumbai 400086",
                Distance = "1.5 km",
                Amenities = "M Ticket,Food & Beverage",
                CancellationPolicy = "FREE CANCELLATION · Available until 15min before show"
            },
            new Theatre
            {
                TheatreId = 3,
                Name = "Carnival Cinemas: Sangam",
                Location = "Andheri East, Mumbai",
                Address = "J.R. Nagar, Andheri East, Mumbai, Maharashtra 400059",
                Distance = "5.8 km",
                Amenities = "M Ticket,Cancellation",
                CancellationPolicy = "MINI CANCELLATION · Available until 15min before show"
            },
            new Theatre
            {
                TheatreId = 4,
                Name = "Cinepolis: Viviana Mall",
                Location = "Thane West",
                Address = "Eastern Express Hwy, Laxmi Nagar, Thane West, Thane 400606",
                Distance = "12.4 km",
                Amenities = "M Ticket,Food & Beverage,Cancellation",
                CancellationPolicy = "MINI CANCELLATION · Available until 20 min before show"
            }
        );

        // Seed Shows (for today and next 6 days, for movie 1, theatres 1-4)
        var today = DateTime.UtcNow.Date;
        var showId = 1;
        for (var d = 0; d < 7; d++)
        {
            var showDate = today.AddDays(d);

            // Movie 1 (Celestial Odyssey) - Theatre 1 shows
            modelBuilder.Entity<Show>().HasData(
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 1, ShowTime = showDate.AddHours(10).AddMinutes(30), Format = "2D", Language = "English", BasePrice = 350, Availability = "Available" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 1, ShowTime = showDate.AddHours(13).AddMinutes(45), Format = "4K ATMOS", Language = "English", BasePrice = 450, Availability = "Filling Fast" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 1, ShowTime = showDate.AddHours(19).AddMinutes(30), Format = "IMAX 2D", Language = "English", BasePrice = 500, Availability = "Available" },
                // Theatre 2
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 2, ShowTime = showDate.AddHours(9).AddMinutes(0), Format = "IMAX 3D", Language = "English", BasePrice = 500, Availability = "Available" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 2, ShowTime = showDate.AddHours(12).AddMinutes(15), Format = "IMAX 3D", Language = "Hindi", BasePrice = 480, Availability = "Filling Fast" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 2, ShowTime = showDate.AddHours(18).AddMinutes(45), Format = "IMAX 2D", Language = "English", BasePrice = 460, Availability = "Available" },
                // Theatre 3
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 3, ShowTime = showDate.AddHours(11).AddMinutes(0), Format = "2D", Language = "Hindi", BasePrice = 280, Availability = "Available" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 3, ShowTime = showDate.AddHours(14).AddMinutes(0), Format = "2D", Language = "English", BasePrice = 300, Availability = "Available" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 3, ShowTime = showDate.AddHours(20).AddMinutes(0), Format = "2D", Language = "Tamil", BasePrice = 280, Availability = "Available" },
                // Theatre 4
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 4, ShowTime = showDate.AddHours(10).AddMinutes(15), Format = "4DX", Language = "English", BasePrice = 600, Availability = "Available" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 4, ShowTime = showDate.AddHours(16).AddMinutes(45), Format = "4DX", Language = "Hindi", BasePrice = 580, Availability = "Filling Fast" },
                new Show { ShowId = showId++, MovieId = 1, TheatreId = 4, ShowTime = showDate.AddHours(20).AddMinutes(0), Format = "IMAX 2D", Language = "English", BasePrice = 500, Availability = "Available" },
                // Movie 2 shows (Theatre 1 and 2 only)
                new Show { ShowId = showId++, MovieId = 2, TheatreId = 1, ShowTime = showDate.AddHours(11).AddMinutes(0), Format = "IMAX 2D", Language = "English", BasePrice = 500, Availability = "Available" },
                new Show { ShowId = showId++, MovieId = 2, TheatreId = 1, ShowTime = showDate.AddHours(18).AddMinutes(30), Format = "4DX", Language = "Hindi", BasePrice = 600, Availability = "Filling Fast" },
                new Show { ShowId = showId++, MovieId = 2, TheatreId = 2, ShowTime = showDate.AddHours(15).AddMinutes(0), Format = "IMAX 3D", Language = "English", BasePrice = 520, Availability = "Available" },
                // Movie 3 shows
                new Show { ShowId = showId++, MovieId = 3, TheatreId = 1, ShowTime = showDate.AddHours(16).AddMinutes(0), Format = "2D", Language = "English", BasePrice = 350, Availability = "Available" },
                new Show { ShowId = showId++, MovieId = 3, TheatreId = 3, ShowTime = showDate.AddHours(19).AddMinutes(30), Format = "2D", Language = "Hindi", BasePrice = 280, Availability = "Available" }
            );
        }

        // Seed Seats for Theatre 1 (12 cols x 10 rows = 120 seats)
        var seatId = 1;
        var rows = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        var premiumRows = new[] { "H", "I", "J" };
        for (var t = 1; t <= 4; t++)
        {
            foreach (var row in rows)
            {
                for (var col = 1; col <= 12; col++)
                {
                    modelBuilder.Entity<Seat>().HasData(new Seat
                    {
                        SeatId = seatId++,
                        TheatreId = t,
                        SeatNumber = $"{row}{col}",
                        Row = row,
                        Col = col,
                        SeatType = Array.Exists(premiumRows, p => p == row) ? "Premium" : "Standard"
                    });
                }
            }
        }
    }
}
