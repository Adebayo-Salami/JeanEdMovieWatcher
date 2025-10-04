import React, { useEffect, useState } from "react";
import MovieCard from "./MovieCard";
import { Movie } from "../types/movie";
import MovieModal from "./MovieModal";
import MovieService from "../services/movie-service";

interface Props {
  movies: Movie[];
}

const MovieList: React.FC<Props> = ({ movies }) => {
  const [movieId, setMovieId] = useState<string>();
  const [movie, setMovie] = useState<Movie>();
  const [showMovieInfo, setShowMovieInfo] = useState(false);

  const onMovieSelected = (movieId: string) => {
    setMovieId(movieId);
  };

  const onCloseMovieInfo = () => {
    setShowMovieInfo(false);
    setMovie(undefined);
    setMovieId(undefined);
  };

  useEffect(() => {
    if (movieId) {
      new MovieService().GetMovieDetail(movieId).then((resp) => {
        setMovie(resp.data.payload);
        setShowMovieInfo(true);
      });
    }
  }, [movieId]);

  return (
    <>
      {showMovieInfo && movie && (
        <MovieModal
          show={showMovieInfo}
          movie={movie}
          onClose={onCloseMovieInfo}
        />
      )}

      <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
        {movies.map((movie, idx) => (
          <div className="col" key={idx}>
            <MovieCard onSelected={onMovieSelected} movie={movie} />
          </div>
        ))}
      </div>
    </>
  );
};

export default MovieList;
