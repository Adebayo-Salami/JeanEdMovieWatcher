import React from "react";
import { Movie } from "../types/movie";
import "../styles/MovieCard.css";

export interface Props {
  movie: {
    imdbID: string;
    poster: string;
    title: string;
    genre: string;
    year: string;
  };
  onSelected: (movieId: string) => void;
}

const MovieCard: React.FC<Props> = ({ movie, onSelected }) => (
  <div
    className="card h-100 shadow-sm movie-card"
    style={{ cursor: "pointer" }}
    onClick={() => onSelected(movie.imdbID)}
  >
    <img
      src={movie.poster}
      className="card-img-top"
      alt={movie.title}
      style={{ objectFit: "cover", height: "300px" }}
    />
    <div className="card-body d-flex flex-column">
      <h5 className="card-title mb-2">{movie.title}</h5>
      <p className="card-text text-muted mb-1">{movie.genre}</p>
      <p className="card-text">
        <small className="text-secondary">{movie.year}</small>
      </p>
    </div>
  </div>
);

export default MovieCard;
