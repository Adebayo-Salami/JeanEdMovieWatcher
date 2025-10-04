import { ApiResponseCodes } from "./enums";

export interface ApiResponse<T> {
  responseCode: string;
  hasErrors: boolean;
  code: ApiResponseCodes;
  payload: T;
  description: string;
  errors: string[];
}
