import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface BookingRequest {
  userId: number;
  showId: number;
  seatIds: number[];
  totalAmount: number;
}

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private apiUrl = 'https://localhost:5001/api/bookings';

  constructor(private http: HttpClient) {}

  // Create booking
  CreateBooking(request: BookingRequest): Observable<any> {
    return this.http.post(this.apiUrl, request);
  }

  // Get booking details
  GetBooking(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  // Cancel booking
  CancelBooking(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}