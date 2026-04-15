import { Component, Input } from '@angular/core';
import { MovieCardComponent } from '../movie-card/movie-card';

@Component({
  selector: 'app-movie-section',
  standalone: true,
  imports: [MovieCardComponent],
  templateUrl: './movie-section.html',
  styleUrls: ['./movie-section.css']
})
export class MovieSectionComponent {
  @Input() title!: string;
  @Input() movies: any[] = [];
}