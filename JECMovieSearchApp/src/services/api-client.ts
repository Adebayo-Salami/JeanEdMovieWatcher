import axios, { CanceledError } from "axios";

export default axios.create({
  baseURL: "https://localhost:5342",
});

export { CanceledError };
