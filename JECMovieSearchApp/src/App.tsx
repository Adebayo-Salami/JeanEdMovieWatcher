import { useEffect, useState } from "react";
import reactLogo from "./assets/react.svg";
import "./App.css";
import MovieList from "./components/MovieList";
import { Movie } from "./types/movie";
import MovieService from "./services/movie-service";

function App() {
  const [movies, setMovies] = useState<Movie[]>([]);
  const [keyword, setKeyword] = useState("Boy");
  const [isLoading, setLoading] = useState(false);

  useEffect(() => {
    if (keyword) {
      setLoading(true);
      new MovieService()
        .Search({ title: keyword })
        .then((response) => {
          setMovies(response.data.payload);
        })
        .catch((error) => {
          console.error("Error fetching movies:", error);
        })
        .finally(() => {
          setLoading(false);
        });
    }
  }, [keyword]);

  return (
    <>
      <MovieList movies={movies} />
    </>
  );
}

export default App;
