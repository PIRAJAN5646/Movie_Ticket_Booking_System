import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Header } from '../../shared/header/header';
import { FooterComponent } from '../../shared/footer/footer';
import { BookingService } from '../../Services/booking';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-booking-confirmation',
  standalone: true,
  imports: [Header, FooterComponent, RouterLink, DatePipe],
  templateUrl: './booking-confirmation.html',
  styleUrls: ['./booking-confirmation.css']
})
export class BookingConfirmationComponent implements OnInit {
  referenceCode = '';
  bookingId = 0;
  bookingDetail: any = null;
  isLoading = signal(true);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookingService: BookingService
  ) {}

  ngOnInit() {
    this.referenceCode = this.route.snapshot.queryParamMap.get('ref') ?? 'CB20240000';
    this.bookingId = Number(this.route.snapshot.queryParamMap.get('bookingId') ?? 0);

    if (this.bookingId > 0) {
      this.bookingService.getBooking(this.bookingId).pipe(
        catchError(() => of(null))
      ).subscribe(data => {
        this.bookingDetail = data;
        this.isLoading.set(false);
      });
    } else {
      this.isLoading.set(false);
    }
  }

  goHome() {
    this.router.navigate(['/']);
  }
}
