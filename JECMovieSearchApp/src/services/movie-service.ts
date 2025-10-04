import { ApiResponse } from "../types/api-response";
import { Movie, MovieSearchVM } from "../types/movie";
import { SearchQuery } from "../types/search-query";
import UtilityService from "./utility-service";
import apiClient from "./api-client";

export default class MovieService {
  GetMovieDetail(movieId: string) {
    return apiClient.get<ApiResponse<Movie>>("/GetMovieDetail/" + movieId);
  }

  GetSearchHistory() {
    return apiClient.get<ApiResponse<SearchQuery[]>>("/GetSearchHistory");
  }

  Search(criterias: MovieSearchVM) {
    return apiClient.get<Movie[]>(
      "Search" + new UtilityService().FormQueryParametersFromCriteria(criterias)
    );
  }
}
