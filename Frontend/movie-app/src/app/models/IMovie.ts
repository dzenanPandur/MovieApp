import { Rating } from "./IRating";

export default interface Movie {
  id: number;
  title: string;
  type: MediaType;
  coverImage: string;
  shortDescription: string;
  releaseDate: string;
  cast: string[];
  ratings: Rating[];
}

export enum MediaType {
  Movie = 0,
  TvShow = 1,
}
