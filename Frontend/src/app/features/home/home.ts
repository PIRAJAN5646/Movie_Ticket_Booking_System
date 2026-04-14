import { Component } from '@angular/core';
import { HeroBannerComponent } from './components/hero-banner/hero-banner';
import { CategoryFilterComponent } from './components/category-filter/category-filter';
import { MovieSectionComponent } from './components/movie-section/movie-section';
import { StreamSectionComponent } from './components/stream-section/stream-section';
import { MembershipBannerComponent } from './components/membership-banner/membership-banner';
import { Header } from '../../shared/header/header';

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
  ],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class HomeComponent {
  movies = Array.from({ length: 20 }, (_, i) => ({
  id: i + 1,
  title: `Movie ${i + 1}`,
  genre: ['Action', 'Drama', 'Comedy', 'Sci-Fi', 'Horror'][i % 5],
  rating: (Math.random() * 3 + 7).toFixed(1),
  imageUrl: `https://picsum.photos/300/400?random=${i + 1}`
}));
newArrivals = Array.from({ length: 20 }, (_, i) => ({
  id: i + 101,
  title: `New Movie ${i + 1}`,
  genre: ['Adventure', 'Thriller', 'Romance'][i % 3],
  rating: (Math.random() * 3 + 7).toFixed(1),
  imageUrl: `https://picsum.photos/300/400?random=${i + 101}`
}));
ngOnInit() {
  console.log(this.movies);
}
}