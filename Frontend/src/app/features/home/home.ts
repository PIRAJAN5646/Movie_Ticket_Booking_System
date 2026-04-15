import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HeroBannerComponent } from './components/hero-banner/hero-banner';
import { CategoryFilterComponent } from './components/category-filter/category-filter';
import { MovieSectionComponent } from './components/movie-section/movie-section';
import { StreamSectionComponent } from './components/stream-section/stream-section';
import { MembershipBannerComponent } from './components/membership-banner/membership-banner';
import { Header } from '../../shared/header/header';
import { FooterComponent } from '../../shared/footer/footer';
import { MovieService } from '../../Services/movies';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    HeroBannerComponent,
    CategoryFilterComponent,
    MovieSectionComponent,
    StreamSectionComponent,
    MembershipBannerComponent,
    Header,
    FooterComponent,
    
  ],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class HomeComponent implements OnInit {
  // Fallback mock data
  private mockMovies = [
    { id: 1, title: 'Avengers: Doomsday', genre: 'Action · Adventure', rating: '9.1', imageUrl: 'https://picsum.photos/seed/avengers/300/440', votes: '128K', pgRating: 'UA' },
    { id: 2, title: 'Neon Nights', genre: 'Sci-Fi · Thriller', rating: '8.6', imageUrl: 'https://picsum.photos/seed/neon/300/440', votes: '74K', pgRating: 'A' },
    { id: 3, title: 'Whispering Ghosts', genre: 'Horror · Mystery', rating: '8.2', imageUrl: 'https://picsum.photos/seed/ghost/300/440', votes: '52K', pgRating: 'A' },
    { id: 4, title: 'The Last Laugh', genre: 'Comedy · Drama', rating: '7.9', imageUrl: 'https://picsum.photos/seed/comedy/300/440', votes: '41K', pgRating: 'U' },
    { id: 5, title: 'Quantum Tide', genre: 'Action · Sci-Fi', rating: '8.8', imageUrl: 'https://picsum.photos/seed/quantum/300/440', votes: '89K', pgRating: 'UA' },
    { id: 6, title: 'Velocity X2', genre: 'Action · Drama', rating: '8.3', imageUrl: 'https://picsum.photos/seed/velocity/300/440', votes: '65K', pgRating: 'UA' },
    { id: 7, title: 'Kingdom Fallen', genre: 'Fantasy · Adventure', rating: '8.7', imageUrl: 'https://picsum.photos/seed/kingdom/300/440', votes: '93K', pgRating: 'UA' },
    { id: 8, title: 'Infinite Loop', genre: 'Sci-Fi · Thriller', rating: '8.0', imageUrl: 'https://picsum.photos/seed/loop/300/440', votes: '47K', pgRating: 'UA' },
  ];

  private mockNewArrivals = [
    { id: 9, title: 'Shattered Mirror', genre: 'Drama · Mystery', rating: '8.4', imageUrl: 'https://picsum.photos/seed/mirror/300/440', votes: '33K', pgRating: 'UA' },
    { id: 10, title: 'Desert Sunrise', genre: 'Adventure · Drama', rating: '8.1', imageUrl: 'https://picsum.photos/seed/desert/300/440', votes: '28K', pgRating: 'U' },
    { id: 11, title: 'City of Justice', genre: 'Action · Crime', rating: '8.5', imageUrl: 'https://picsum.photos/seed/city/300/440', votes: '61K', pgRating: 'A' },
    { id: 12, title: 'Nova Frontier', genre: 'Sci-Fi · Action', rating: '8.9', imageUrl: 'https://picsum.photos/seed/nova/300/440', votes: '72K', pgRating: 'UA' },
  ];

  movies: any[] = this.mockMovies;
  newArrivals: any[] = this.mockNewArrivals;
  isLoading = true;

  constructor(private movieService: MovieService) {}

  ngOnInit() {
    // Load movies from API (fallback to mock if API down)
    this.movieService.getMovies().pipe(
      catchError(() => of(null))
    ).subscribe(data => {
      if (data && data.length > 0) {
        this.movies = data.map(m => ({
          id: m.movieId,
          title: m.title,
          genre: m.genre,
          rating: m.rating,
          imageUrl: m.posterUrl,
          votes: m.votes,
          pgRating: m.pgRating
        }));
        this.newArrivals = this.movies.slice(-4);
      }
      this.isLoading = false;
    });
  }
}