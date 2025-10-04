import { MovieRating } from "./movie-rating";

export interface Movie {
  imdbID?: string;
  poster: string;
  title: string;
  year: string;
  rated?: string;
  released?: string;
  runtime?: string;
  genre: string;
  type?: string;
  imdbRating?: string;
  imdbVotes?: string;
  country?: string;
  director?: string;
  writer?: string;
  actors?: string;
  awards?: string;
  ratings?: MovieRating[];
}

export interface MovieSearchVM {
  title: string;
}
