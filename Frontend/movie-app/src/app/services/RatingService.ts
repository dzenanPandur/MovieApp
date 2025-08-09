import { HttpStatusCode } from "axios";
import networkService from "./NetworkService";

class RatingService {
    private readonly STORAGE_KEY = 'rated_movies_session';

    private getRatedMovies(): Set<number> {
        const stored = localStorage.getItem(this.STORAGE_KEY);
        return new Set(stored ? JSON.parse(stored) : []);
    }

    private saveRatedMovies(ratedMovies: Set<number>): void {
        localStorage.setItem(this.STORAGE_KEY, JSON.stringify([...ratedMovies]));
    }

 
    async addRating(movieId: number, rating: number): Promise<void> {
        if (this.hasRated(movieId)) {
            throw new Error('You have already rated this movie in this session');
        }

      
        const result = await networkService.post(`/Movie/movies/${movieId}/ratings`, rating);

        if (!result.isSuccessful) {
            throw result.data;
        }


        const ratedMovies = this.getRatedMovies();
        ratedMovies.add(movieId);
        this.saveRatedMovies(ratedMovies);
    }


    hasRated(movieId: number): boolean {
        return this.getRatedMovies().has(movieId);
    }


    clearSession(): void {
        localStorage.removeItem(this.STORAGE_KEY);
    }
}

const ratingService = new RatingService();
export default ratingService;