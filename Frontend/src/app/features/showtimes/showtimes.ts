import { Component, OnInit, signal, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Header } from '../../shared/header/header';
import { FooterComponent } from '../../shared/footer/footer';
import { ShowService } from '../../Services/show';
import { MovieService } from '../../Services/movies';

interface ShowtimeEntry {
  showId: number;
  time: string;
  format: string;
  availability: 'Available' | 'Filling Fast' | 'Sold Out';
  basePrice: number;
  language: string;
}

interface TheatreGroup {
  theatreId: number;
  name: string;
  address: string;
  distance: string;
  amenities: string[];
  cancellationPolicy: string;
  showtimes: ShowtimeEntry[];
}

@Component({
  selector: 'app-showtimes',
  standalone: true,
  imports: [Header, FooterComponent],
  templateUrl: './showtimes.html',
  styleUrls: ['./showtimes.css']
})
export class ShowtimesComponent implements OnInit {
  movieId = signal(1);
  movieTitle = signal('');
  movieMeta = signal('');

  dates: { label: string; day: string; date: string; month: string; full: string; isoDate: string }[] = [];
  selectedDateIndex = signal(0);
  selectedLanguage = signal('All');
  selectedFormat = signal('All');

  // Use signals so Angular always detects changes
  theatres = signal<TheatreGroup[]>([]);
  isLoading = signal(true);
  errorMsg = signal('');

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private showService: ShowService,
    private movieService: MovieService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id')) || 1;
      this.movieId.set(id);
      this.buildDates();
      this.loadMovieTitle(id);
      this.loadShows();
    });
  }

  private buildDates() {
    this.dates = [];
    const today = new Date();
    const days = ['SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT'];
    const months = ['JAN','FEB','MAR','APR','MAY','JUN','JUL','AUG','SEP','OCT','NOV','DEC'];
    for (let i = 0; i < 7; i++) {
      const d = new Date(today);
      d.setDate(today.getDate() + i);
      const localISO = `${d.getFullYear()}-${String(d.getMonth()+1).padStart(2,'0')}-${String(d.getDate()).padStart(2,'0')}`;
      this.dates.push({
        label: i === 0 ? 'TODAY' : '',
        day: days[d.getDay()],
        date: String(d.getDate()),
        month: months[d.getMonth()],
        full: d.toLocaleDateString(),
        isoDate: localISO
      });
    }
  }

  private loadMovieTitle(id: number) {
    this.movieService.getMovie(id).subscribe({
      next: m => {
        if (m) {
          this.movieTitle.set(m.title);
          const genreParts = m.genre?.split(' · ') ?? [];
          const h = Math.floor(m.duration / 60);
          const mins = m.duration % 60;
          this.movieMeta.set(`${m.pgRating} · ${genreParts.join(' · ')} · ${h}h ${mins}m`);
        }
      },
      error: () => {
        this.movieTitle.set(`Movie ${id}`);
        this.movieMeta.set('UA · Action');
      }
    });
  }

  loadShows() {
    this.isLoading.set(true);
    this.errorMsg.set('');
    const selectedDate = this.dates[this.selectedDateIndex()]?.isoDate;
    const lang = this.selectedLanguage() !== 'All' ? this.selectedLanguage() : undefined;
    const fmt = this.selectedFormat() !== 'All' ? this.selectedFormat() : undefined;

    this.showService.getShowsByMovie(this.movieId(), selectedDate, lang, fmt).subscribe({
      next: (data) => {
        if (data && data.length > 0) {
          this.theatres.set(data.map((t: any) => ({
            theatreId: t.theatreId,
            name: t.theatreName,
            address: t.address + (t.distance ? ' · ' + t.distance : ''),
            distance: t.distance ?? '',
            amenities: t.amenities ?? ['M Ticket', 'Food & Beverage'],
            cancellationPolicy: t.cancellationPolicy ?? 'MINI CANCELLATION · Available until 20 min before show',
            showtimes: (t.shows ?? []).map((s: any) => ({
              showId: s.showId,
              time: new Date(s.showTime).toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit' }),
              format: s.format,
              availability: s.availability as any,
              basePrice: s.basePrice,
              language: s.language
            }))
          })));
          this.errorMsg.set('');
        } else {
          this.theatres.set([]);
          this.errorMsg.set(selectedDate
            ? `No shows found for ${selectedDate}. Try a different date.`
            : 'No shows available.');
        }
        this.isLoading.set(false);
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Shows API error:', err);
        this.theatres.set([]);
        this.errorMsg.set('Unable to load shows. Please check your connection.');
        this.isLoading.set(false);
        this.cdr.detectChanges();
      }
    });
  }

  selectDate(i: number) {
    this.selectedDateIndex.set(i);
    this.loadShows();
  }

  bookShow(theatreId: number, showtime: ShowtimeEntry) {
    if (showtime.availability !== 'Sold Out') {
      this.router.navigate(['/movie', this.movieId(), 'showtimes', showtime.showId, 'seats']);
    }
  }
}
