import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Header } from '../../shared/header/header';
import { FooterComponent } from '../../shared/footer/footer';
import { MovieService } from '../../Services/movies';

@Component({
  selector: 'app-movie-detail',
  standalone: true,
  imports: [Header, FooterComponent, RouterLink],
  templateUrl: './movie-detail.html',
  styleUrls: ['./movie-detail.css']
})
export class MovieDetailComponent implements OnInit {
  movie: any = null;
  movieId = 1;
  isLoading = true;
  error = false;

  constructor(private route: ActivatedRoute, private movieService: MovieService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.movieId = Number(params.get('id')) || 1;
      this.isLoading = true;
      this.error = false;

      this.movieService.getMovie(this.movieId).subscribe({
        next: data => {
          if (data) {
            this.movie = {
              id: data.movieId,
              title: data.title,
              badge: data.badge,
              rating: data.rating,
              votes: data.votes,
              formats: data.formats?.split(',') ?? ['2D'],
              languages: data.languages?.split(',') ?? ['English'],
              genres: data.genre?.split(' · ') ?? ['Action'],
              duration: this.formatDuration(data.duration),
              releaseDate: new Date(data.releaseDate).toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' }),
              pgRating: data.pgRating,
              description: data.description,
              posterUrl: data.posterUrl,
              backdropUrl: data.backdropUrl,
              cast: this.parseCast(data.cast),
              crew: this.parseCrew(data.crew),
              similar: [
                { id: this.movieId > 1 ? this.movieId - 1 : 2, imageUrl: `https://picsum.photos/seed/s${this.movieId}1/200/280` },
                { id: this.movieId < 8 ? this.movieId + 1 : 1, imageUrl: `https://picsum.photos/seed/s${this.movieId}2/200/280` },
              ]
            };
          } else {
            this.movie = this.getFallbackMovie(this.movieId);
          }
          this.isLoading = false;
          this.cdr.detectChanges();
        },
        error: () => {
          this.movie = this.getFallbackMovie(this.movieId);
          this.error = true;
          this.isLoading = false;
          this.cdr.detectChanges();
        }
      });
    });
  }

  private formatDuration(minutes: number): string {
    const h = Math.floor(minutes / 60);
    const m = minutes % 60;
    return `${h}h ${m}m`;
  }

  private parseCast(castJson: string): any[] {
    try { return JSON.parse(castJson); } catch { return []; }
  }

  private parseCrew(crewJson: string): any {
    try { return JSON.parse(crewJson); } catch { return {}; }
  }

  private getFallbackMovie(id: number) {
    return {
      id,
      title: `Movie ${id}`,
      badge: '',
      rating: '8.5',
      votes: '42K',
      formats: ['2D', 'IMAX 2D'],
      languages: ['English', 'Hindi'],
      genres: ['Action', 'Drama'],
      duration: '2h 15m',
      releaseDate: 'Nov 10, 2024',
      pgRating: 'UA',
      description: 'An incredible cinematic experience. A must-watch for all movie lovers.',
      posterUrl: `https://picsum.photos/seed/movie${id}/300/450`,
      backdropUrl: `https://picsum.photos/seed/back${id}/1400/500`,
      cast: [
        { name: 'Lead Actor', role: 'Hero', img: `https://picsum.photos/seed/c${id}1/80/80` },
        { name: 'Supporting Lead', role: 'Co-Star', img: `https://picsum.photos/seed/c${id}2/80/80` },
      ],
      crew: { director: 'Director Name', producer: 'Producer Name', music: 'Music Director', writer: 'Screen Writer' },
      similar: [
        { id: id > 1 ? id - 1 : 2, imageUrl: `https://picsum.photos/seed/s${id}1/200/280` },
        { id: id < 8 ? id + 1 : 1, imageUrl: `https://picsum.photos/seed/s${id}2/200/280` },
      ]
    };
  }
}
