export interface Movie {
  movieId: number;
  title: string;
  genre: string;
  duration: number;
  description: string;
  language: string;
  releaseDate: string;
  posterUrl: string;
  backdropUrl: string;
  rating: string;
  votes: string;
  pgRating: string;
  badge: string;
  formats: string;
  languages: string;
  cast: string;  // JSON string
  crew: string;  // JSON string
}
