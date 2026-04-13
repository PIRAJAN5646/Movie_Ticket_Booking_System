import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:5001/api/users';

  constructor(private http: HttpClient) {}

  // Register user
  register(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, user);
  }

  // Get user profile
  getProfile(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}/profile`);
  }

}