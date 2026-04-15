import { Component, OnInit, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Header } from '../../shared/header/header';
import { FooterComponent } from '../../shared/footer/footer';
import { BookingStateService } from '../../Services/booking-state.service';
import { BookingService } from '../../Services/booking';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-booking-summary',
  standalone: true,
  imports: [Header, FooterComponent],
  templateUrl: './booking-summary.html',
  styleUrls: ['./booking-summary.css']
})
export class BookingSummaryComponent implements OnInit {
  state: any = null;
  isProcessing = signal(false);
  bookingError = signal('');

  steps = [
    { num: 1, label: 'Showtimes' },
    { num: 2, label: 'Seats' },
    { num: 3, label: 'Summary' },
  ];
  currentStep = 3;

  // Fallback mock data
  private mockBooking = {
    movie: {
      title: 'The Celestial Odyssey',
      language: 'English',
      format: 'IMAX 2D',
      genres: ['Sci-Fi', 'Adventure'],
      pgRating: 'UA | 13+',
      imageUrl: 'https://picsum.photos/seed/celestial/120/170',
    },
    date: 'Sunday, 24 Oct 2024',
    time: '07:30 PM',
    venue: 'PVR: ICON, Phoenix Palladium',
    seats: 'J12, J13',
    screen: 'SCREEN 1',
    ticketCount: 2,
    ticketPrice: 950,
    convenienceFee: 78.4,
    gst: 14.1,
  };

  constructor(
    public bookingStateService: BookingStateService,
    private bookingService: BookingService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.state = this.bookingStateService.state();
  }

  get hasMockData() { return !this.state; }

  get booking() {
    if (!this.state) return this.mockBooking;
    const showDt = new Date(this.state.showTime);
    return {
      movie: {
        title: this.state.movieTitle,
        language: this.state.language,
        format: this.state.format,
        genres: this.state.movieGenres,
        pgRating: this.state.moviePgRating,
        imageUrl: this.state.moviePosterUrl,
      },
      date: showDt.toLocaleDateString('en-US', { weekday: 'long', day: 'numeric', month: 'short', year: 'numeric' }),
      time: showDt.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit' }),
      venue: this.state.theatreName,
      seats: this.state.selectedSeats.map((s: any) => s.seatNumber).join(', '),
      screen: this.state.screen,
      ticketCount: this.state.selectedSeats.length,
      ticketPrice: this.bookingStateService.getTicketTotal(),
      convenienceFee: this.bookingStateService.getConvenienceFee(),
      gst: this.bookingStateService.getGst(),
    };
  }

  get totalAmount() {
    if (!this.state) return this.mockBooking.ticketPrice * this.mockBooking.ticketCount + this.mockBooking.convenienceFee + this.mockBooking.gst;
    return this.bookingStateService.getTotalAmount();
  }

  proceedToPay() {
    if (!this.state) {
      // Demo: just navigate to confirmation with mock reference
      this.router.navigate(['/booking/confirmation'], { queryParams: { ref: 'CB20240101DEMO', bookingId: 0 } });
      return;
    }

    this.isProcessing.set(true);
    this.bookingError.set('');

    const user = this.authService.currentUser();
    const request = {
      userId: user?.userId ?? 0,
      showId: this.state.showId,
      seatIds: this.state.selectedSeats.map((s: any) => s.seatId).filter((id: number) => id > 0),
      totalAmount: this.totalAmount
    };

    // If no real seatIds (fallback), simulate success
    if (request.seatIds.length === 0) {
      this.bookingStateService.clearState();
      this.router.navigate(['/booking/confirmation'], { queryParams: { ref: 'CB' + Date.now(), bookingId: 0 } });
      return;
    }

    this.bookingService.createBooking(request).subscribe({
      next: (res) => {
          this.isProcessing.set(false);
          // Update wallet in header
          if (res.walletBalance !== undefined && this.authService.isLoggedIn()) {
            this.authService.updateWalletBalance(res.walletBalance);
          }
          this.bookingStateService.clearState();
          this.router.navigate(['/booking/confirmation'], { queryParams: { ref: res.referenceCode, bookingId: res.bookingId } });
        },
      error: (err) => {
        this.isProcessing.set(false);
        this.bookingError.set(err?.error?.message ?? 'Booking failed. Please try again.');
      }
    });
  }

  changeSeat() {
    if (this.state) {
      this.router.navigate(['/movie', this.state.movieId, 'showtimes', this.state.showId, 'seats']);
    }
  }
}
