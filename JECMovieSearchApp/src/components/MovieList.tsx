import React from "react";
import MovieCard from "./MovieCard";
import { Movie } from "../types/movie";

interface Props {
  movies: Movie[];
}

const MovieList: React.FC<Props> = ({ movies }) => (
  <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
    {movies.map((movie, idx) => (
      <div className="col" key={idx}>
        <MovieCard {...movie} />
      </div>
    ))}
  </div>
);

export default MovieList;
