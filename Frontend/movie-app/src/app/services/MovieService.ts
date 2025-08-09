import { HttpStatusCode } from "axios";
import IMovie from "../models/IMovie";
import networkService from "./NetworkService";

class MovieService {

    async getMovies(): Promise<IMovie[]> {
        const result = await networkService.get<IMovie[]>(`/Movie/movies`);

        if (!result.isSuccessful) {
            throw result.data;
        }

        if (result.status === HttpStatusCode.NoContent) {
            return [];
        }


        return result.data as IMovie[];
    }

  
    async getMovie(id: number): Promise<IMovie> {
        const result = await networkService.get<IMovie>(`/Movie/movies/${id}`);

        if (result.isSuccessful) {
            return result.data as IMovie;
        }

        throw result.data;
    }
}

const movieService = new MovieService();
export default movieService;