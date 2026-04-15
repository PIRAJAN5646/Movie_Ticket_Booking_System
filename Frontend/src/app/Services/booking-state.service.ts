import { Injectable, signal } from '@angular/core';

export interface BookingState {
  movieId: number;
  showId: number;
  movieTitle: string;
  moviePosterUrl: string;
  movieGenres: string[];
  moviePgRating: string;
  theatreName: string;
  showTime: Date;
  format: string;
  language: string;
  selectedSeats: SelectedSeat[];
  screen: string;
}

export interface SelectedSeat {
  seatId: number;
  seatNumber: string;
  seatType: string;
  price: number;
}

@Injectable({ providedIn: 'root' })
export class BookingStateService {
  private _state = signal<BookingState | null>(null);

  state = this._state.asReadonly();

  setState(state: BookingState) {
    this._state.set(state);
  }

  clearState() {
    this._state.set(null);
  }

  getTicketTotal(): number {
    return this._state()?.selectedSeats.reduce((sum, s) => sum + s.price, 0) ?? 0;
  }

  getConvenienceFee(): number {
    return Math.round(this.getTicketTotal() * 0.0413 * 100) / 100;
  }

  getGst(): number {
    return Math.round(this.getTicketTotal() * 0.018 * 100) / 100;
  }

  getTotalAmount(): number {
    return Math.round((this.getTicketTotal() + this.getConvenienceFee() + this.getGst()) * 100) / 100;
  }
}
