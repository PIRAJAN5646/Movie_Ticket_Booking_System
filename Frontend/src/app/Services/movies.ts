import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../Models/movie';

@Injectable({ providedIn: 'root' })
export class MovieService {
  private apiUrl = '/api/movies';

  constructor(private http: HttpClient) {}

  getMovies(search?: string, genre?: string): Observable<Movie[]> {
    const params: any = {};
    if (search) params.search = search;
    if (genre) params.genre = genre;
    return this.http.get<Movie[]>(this.apiUrl, { params });
  }

  getMovie(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.apiUrl}/${id}`);
  }

  getNewArrivals(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.apiUrl}/new-arrivals`);
  }
}