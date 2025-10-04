import { ApiResponse } from "../types/api-response";
import { Movie, MovieSearchVM } from "../types/movie";
import { SearchQuery } from "../types/search-query";
import UtilityService from "./utility-service";
import apiClient from "./api-client";
import { AxiosResponse } from "axios";

export default class MovieService {
  GetMovieDetail(movieId: string) {
    return apiClient.get<ApiResponse<Movie>>("/Movie/GetMovieDetail/" + movieId);
  }

  GetSearchHistory() {
    return apiClient.get<ApiResponse<SearchQuery[]>>("/Movie/GetSearchHistory");
  }

  Search(criterias: MovieSearchVM) {
    return apiClient.get<ApiResponse<Movie[]>>(
      "/Movie/Search" + new UtilityService().FormQueryParametersFromCriteria(criterias)
    );
  }
}
