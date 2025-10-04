import React from "react";
import { Movie } from "../types/movie";

const MovieCard: React.FC<Movie> = ({ poster, title, genre, year }) => (
  <div className="card h-100 shadow-sm">
    <img
      src={poster}
      className="card-img-top"
      alt={title}
      style={{ objectFit: "cover", height: "300px" }}
    />
    <div className="card-body d-flex flex-column">
      <h5 className="card-title mb-2">{title}</h5>
      <p className="card-text text-muted mb-1">{genre}</p>
      <p className="card-text">
        <small className="text-secondary">{year}</small>
      </p>
    </div>
  </div>
);

export default MovieCard;
