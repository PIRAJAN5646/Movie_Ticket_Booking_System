namespace Movie_ticket_bookingAPI.DTOs;

public record LoginRequest(string Email, string Password);

public record RegisterRequest(string Name, string Email, string Password, string Phone);

public record AuthResponse(string Token, int UserId, string Name, string Email, decimal WalletBalance);

public record BookingRequest(int UserId, int ShowId, List<int> SeatIds, decimal TotalAmount);

public record CreateBookingResponse(int BookingId, string ReferenceCode, decimal TotalAmount, string Status, decimal WalletBalance);

public class ShowWithSeatsDto
{
    public int ShowId { get; set; }
    public int MovieId { get; set; }
    public int TheatreId { get; set; }
    public string TheatreName { get; set; } = string.Empty;
    public DateTime ShowTime { get; set; }
    public string Format { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public string Availability { get; set; } = string.Empty;
    public List<SeatDto> Seats { get; set; } = new();
}

public class SeatDto
{
    public int SeatId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public string Row { get; set; } = string.Empty;
    public int Col { get; set; }
    public string SeatType { get; set; } = string.Empty;
    public bool IsBooked { get; set; }
    public decimal Price { get; set; }
}

public class BookingDetailDto
{
    public int BookingId { get; set; }
    public string ReferenceCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime BookingTime { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public string MoviePosterUrl { get; set; } = string.Empty;
    public string TheatreName { get; set; } = string.Empty;
    public DateTime ShowTime { get; set; }
    public string Format { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public List<string> Seats { get; set; } = new();
}
