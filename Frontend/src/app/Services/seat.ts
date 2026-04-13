import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Seat } from '../Models/seat';

@Injectable({
  providedIn: 'root'
})
export class SeatService {

  private apiUrl = 'https://localhost:5001/api/seats';

  constructor(private http: HttpClient) {}

  // Get seat layout for a show
  getSeatLayout(showId: number): Observable<Seat[]> {
    return this.http.get<Seat[]>(`${this.apiUrl}/layout/${showId}`);
  }

  // Update seat booking status
  updateSeatStatus(seatId: number, status: boolean): Observable<any> {
    return this.http.put(
      `${this.apiUrl}/${seatId}/status?status=${status}`,
      {}
    );
  }
}