import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-hero-banner',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './hero-banner.html',
  styleUrls: ['./hero-banner.css']
})
export class HeroBannerComponent {
  featuredMovie = {
    id: 1,
    title: 'The Celestial Odyssey',
    description: 'A breathtaking journey across galaxies where humanity discovers its true origin. An epic adventure redefining our place in the cosmos.',
    rating: '9.2',
    votes: '430.2K',
    formats: ['2D', 'IMAX 2D', '4DX'],
    genres: ['Sc-Fi', 'Action', 'Adventure'],
    image: 'https://images.unsplash.com/photo-1534796636912-3b95b3ab5986?w=1400&q=80',
    badge: 'PREMIUM 70MM'
  };
}