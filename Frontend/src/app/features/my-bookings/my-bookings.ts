import { Component, OnInit, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { DatePipe, DecimalPipe } from '@angular/common';
import { Header } from '../../shared/header/header';
import { FooterComponent } from '../../shared/footer/footer';
import { BookingService } from '../../Services/booking';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [Header, FooterComponent, RouterLink, DatePipe, DecimalPipe],
  templateUrl: './my-bookings.html',
  styleUrls: ['./my-bookings.css']
})
export class MyBookingsComponent implements OnInit {
  bookings = signal<any[]>([]);
  isLoading = signal(true);
  errorMsg = signal('');

  constructor(
    private bookingService: BookingService,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    const user = this.auth.currentUser();
    if (!user) {
      this.router.navigate(['/signin']);
      return;
    }

    this.bookingService.getUserBookings(user.userId).subscribe({
      next: (data) => {
        this.bookings.set(data ?? []);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMsg.set('Unable to load your bookings. Please try again.');
        this.isLoading.set(false);
      }
    });
  }

  getStatusClass(status: string) {
    if (status === 'Confirmed') return 'status-confirmed';
    if (status === 'Cancelled') return 'status-cancelled';
    return 'status-pending';
  }

  getStatusIcon(status: string) {
    if (status === 'Confirmed') return '✓';
    if (status === 'Cancelled') return '✕';
    return '⏳';
  }
}
