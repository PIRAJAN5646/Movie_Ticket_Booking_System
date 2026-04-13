import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Theatre } from '../Models/theatre';

@Injectable({
  providedIn: 'root'
})
export class TheatreService {

  private apiUrl = 'https://localhost:5001/api/theatres';

  constructor(private http: HttpClient) {}

  // Get all theatres
  getTheatres(): Observable<Theatre[]> {
    return this.http.get<Theatre[]>(this.apiUrl);
  }

  // Create theatre
  createTheatre(theatre: Theatre): Observable<Theatre> {
    return this.http.post<Theatre>(this.apiUrl, theatre);
  }

}