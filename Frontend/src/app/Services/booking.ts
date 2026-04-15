import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface BookingRequest {
  userId: number;
  showId: number;
  seatIds: number[];
  totalAmount: number;
}

export interface BookingResponse {
  bookingId: number;
  referenceCode: string;
  totalAmount: number;
  status: string;
  walletBalance: number;
}

@Injectable({ providedIn: 'root' })
export class BookingService {
  private apiUrl = '/api/bookings';

  constructor(private http: HttpClient) {}

  createBooking(request: BookingRequest): Observable<BookingResponse> {
    return this.http.post<BookingResponse>(this.apiUrl, request);
  }

  getBooking(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  getUserBookings(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }

  cancelBooking(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}