import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

export interface AuthUser {
  userId: number;
  name: string;
  email: string;
  token: string;
  walletBalance: number;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly TOKEN_KEY = 'cinebook_token';
  private readonly USER_KEY = 'cinebook_user';

  currentUser = signal<AuthUser | null>(this.loadUser());

  constructor(private http: HttpClient, private router: Router) {}

  private loadUser(): AuthUser | null {
    try {
      const raw = localStorage.getItem(this.USER_KEY);
      return raw ? JSON.parse(raw) : null;
    } catch {
      return null;
    }
  }

  login(email: string, password: string): Observable<AuthUser> {
    return this.http.post<AuthUser>('/api/auth/login', { email, password }).pipe(
      tap(user => this.setUser(user))
    );
  }

  register(name: string, email: string, password: string, phone: string): Observable<AuthUser> {
    return this.http.post<AuthUser>('/api/auth/register', { name, email, password, phone }).pipe(
      tap(user => this.setUser(user))
    );
  }

  setUser(user: AuthUser) {
    localStorage.setItem(this.USER_KEY, JSON.stringify(user));
    localStorage.setItem(this.TOKEN_KEY, user.token);
    this.currentUser.set(user);
  }

  updateWalletBalance(newBalance: number) {
    const user = this.currentUser();
    if (user) {
      const updated = { ...user, walletBalance: newBalance };
      localStorage.setItem(this.USER_KEY, JSON.stringify(updated));
      this.currentUser.set(updated);
    }
  }

  logout() {
    localStorage.removeItem(this.USER_KEY);
    localStorage.removeItem(this.TOKEN_KEY);
    this.currentUser.set(null);
    this.router.navigate(['/']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.currentUser();
  }
}
