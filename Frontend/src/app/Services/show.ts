import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ShowService {
  private apiUrl = '/api/shows';

  constructor(private http: HttpClient) {}

  getShowsByMovie(movieId: number, date?: string, language?: string, format?: string): Observable<any[]> {
    const params: any = {};
    if (date) params.date = date;
    if (language) params.language = language;
    if (format) params.format = format;
    return this.http.get<any[]>(`${this.apiUrl}/bymovie/${movieId}`, { params });
  }

  getShowWithSeats(showId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${showId}/seats`);
  }
}