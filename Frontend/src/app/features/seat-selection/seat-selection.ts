import { Component, OnInit, signal, computed, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Header } from '../../shared/header/header';
import { ShowService } from '../../Services/show';
import { BookingStateService } from '../../Services/booking-state.service';

interface Seat {
  seatId: number;
  id: string;
  row: string;
  col: number;
  status: 'available' | 'sold' | 'selected';
  type: 'standard' | 'premium';
  price: number;
}

@Component({
  selector: 'app-seat-selection',
  standalone: true,
  imports: [Header, RouterLink],
  templateUrl: './seat-selection.html',
  styleUrls: ['./seat-selection.css']
})
export class SeatSelectionComponent implements OnInit {
  movieId = signal(1);
  showId = signal(1);

  movieTitle = '';
  theatreName = '';
  showDate = '';
  format = '';
  language = '';
  ticketPrice = 350;
  moviePosterUrl = '';
  moviePgRating = 'UA';
  movieGenres: string[] = [];

  rows = ['J','I','H','G','F','E','D','C','B','A'];
  cols = Array.from({length: 12}, (_, i) => i + 1);

  seats: Seat[][] = [];
  selectedSeats = signal<Seat[]>([]);

  ticketTotal = computed(() => this.selectedSeats().reduce((sum, s) => sum + s.price, 0));
  feesTotal = computed(() => Math.round(this.ticketTotal() * 0.0413 * 100) / 100);
  grandTotal = computed(() => Math.round((this.ticketTotal() + this.feesTotal()) * 100) / 100);

  isLoading = true;
  errorMsg = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private showService: ShowService,
    private bookingState: BookingStateService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    // Use paramMap observable — avoids double-load and reacts to route changes
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id')) || 1;
      const showId = Number(params.get('showId')) || 1;
      this.movieId.set(id);
      this.showId.set(showId);
      this.loadSeats(showId);
    });
  }

  private loadSeats(showId: number) {
    this.isLoading = true;
    this.errorMsg = '';
    this.seats = [];
    this.selectedSeats.set([]);

    this.showService.getShowWithSeats(showId).subscribe({
      next: (data) => {
        if (data) {
          this.theatreName = data.theatreName ?? 'Theatre';
          this.format = data.format ?? '2D';
          this.language = data.language ?? 'English';
          this.ticketPrice = data.basePrice ?? 350;
          const showDt = new Date(data.showTime);
          this.showDate = showDt.toLocaleDateString('en-US', {
            weekday: 'long', day: 'numeric', month: 'short'
          }) + ', ' + showDt.toLocaleTimeString('en-US', {
            hour: '2-digit', minute: '2-digit'
          });
          this.buildSeatsFromApi(data.seats ?? []);
        } else {
          this.generateMockSeats();
        }
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.generateMockSeats();
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  private buildSeatsFromApi(apiSeats: any[]) {
    const seatMap = new Map<string, any>();
    apiSeats.forEach(s => seatMap.set(`${s.row}${s.col}`, s));

    this.seats = this.rows.map(row =>
      this.cols.map(col => {
        const apiSeat = seatMap.get(`${row}${col}`);
        if (apiSeat) {
          return {
            seatId: apiSeat.seatId,
            id: apiSeat.seatNumber,
            row,
            col,
            status: apiSeat.isBooked ? 'sold' as const : 'available' as const,
            type: (apiSeat.seatType === 'Premium' ? 'premium' : 'standard') as 'premium' | 'standard',
            price: apiSeat.price ?? this.ticketPrice
          };
        }
        return {
          seatId: 0, id: `${row}${col}`, row, col,
          status: 'available' as const,
          type: 'standard' as const,
          price: this.ticketPrice
        };
      })
    );
  }

  private generateMockSeats() {
    const soldPositions = new Set(['E7','D4','D8','C5','C6','B3','B9','A6','A7','G4','G5','H2','H10','I3','I9']);
    this.seats = this.rows.map(row =>
      this.cols.map(col => {
        const id = `${row}${col}`;
        const isPremium = ['J','I','H'].includes(row);
        return {
          seatId: 0, id, row, col,
          status: soldPositions.has(id) ? 'sold' as const : 'available' as const,
          type: isPremium ? 'premium' as const : 'standard' as const,
          price: isPremium ? this.ticketPrice + 150 : this.ticketPrice
        };
      })
    );
    if (!this.theatreName) this.theatreName = 'PVR: Icon, Infiniti Mall';
    if (!this.format) this.format = 'IMAX 2D';
    if (!this.language) this.language = 'English';
    if (!this.showDate) this.showDate = new Date().toLocaleDateString('en-US', { weekday: 'long', day: 'numeric', month: 'short' });
  }

  toggleSeat(seat: Seat) {
    if (seat.status === 'sold') return;
    if (seat.status === 'selected') {
      seat.status = 'available';
      this.selectedSeats.update(s => s.filter(x => x.id !== seat.id));
    } else {
      if (this.selectedSeats().length >= 10) return;
      seat.status = 'selected';
      this.selectedSeats.update(s => [...s, seat]);
    }
  }

  removeSelected(seatId: string) {
    const s = this.seats.flat().find(x => x.id === seatId);
    if (s) s.status = 'available';
    this.selectedSeats.update(arr => arr.filter(x => x.id !== seatId));
  }

  proceedToPay() {
    if (this.selectedSeats().length === 0) return;
    this.bookingState.setState({
      movieId: this.movieId(),
      showId: this.showId(),
      movieTitle: this.movieTitle || `Movie ${this.movieId()}`,
      moviePosterUrl: this.moviePosterUrl || `https://picsum.photos/seed/movie${this.movieId()}/120/170`,
      movieGenres: this.movieGenres.length ? this.movieGenres : ['Action'],
      moviePgRating: this.moviePgRating,
      theatreName: this.theatreName,
      showTime: new Date(),
      format: this.format,
      language: this.language,
      screen: 'SCREEN 1',
      selectedSeats: this.selectedSeats().map(s => ({
        seatId: s.seatId,
        seatNumber: s.id,
        seatType: s.type,
        price: s.price
      }))
    });
    this.router.navigate(['/booking/summary']);
  }
}
