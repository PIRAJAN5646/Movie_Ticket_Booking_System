import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'movie/:id',
    loadComponent: () => import('./features/movie-detail/movie-detail').then(m => m.MovieDetailComponent)
  },
  {
    path: 'movie/:id/showtimes',
    loadComponent: () => import('./features/showtimes/showtimes').then(m => m.ShowtimesComponent)
  },
  {
    path: 'movie/:id/showtimes/:showId/seats',
    loadComponent: () => import('./features/seat-selection/seat-selection').then(m => m.SeatSelectionComponent)
  },
  {
    path: 'booking/summary',
    loadComponent: () => import('./features/booking-summary/booking-summary').then(m => m.BookingSummaryComponent)
  },
  {
    path: 'booking/confirmation',
    loadComponent: () => import('./features/booking-confirmation/booking-confirmation').then(m => m.BookingConfirmationComponent)
  },
  {
    path: 'signin',
    loadComponent: () => import('./features/signin/signin').then(m => m.SignInComponent)
  },
  {
    path: 'my-bookings',
    loadComponent: () => import('./features/my-bookings/my-bookings').then(m => m.MyBookingsComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
