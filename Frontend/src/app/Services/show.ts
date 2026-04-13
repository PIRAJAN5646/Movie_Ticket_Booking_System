import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Show } from '../Models/show';

@Injectable({
  providedIn: 'root'
})
export class ShowService {

  private apiUrl = 'https://localhost:5001/api/shows';

  constructor(private http: HttpClient) {}

  // Get shows by movie
  getShowsByMovie(movieId: number): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/bymovie/${movieId}`);
  }

  // Get shows by theatre
  getShowsByTheatre(theatreId: number): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/bytheatre/${theatreId}`);
  }

  // Create new show
  createShow(show: Show): Observable<Show> {
    return this.http.post<Show>(this.apiUrl, show);
  }

}