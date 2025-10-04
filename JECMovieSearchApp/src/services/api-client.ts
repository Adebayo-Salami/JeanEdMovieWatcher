import axios, { CanceledError } from "axios";

export default axios.create({
  baseURL: "https://localhost:7227/api/v1",
});

export { CanceledError };
