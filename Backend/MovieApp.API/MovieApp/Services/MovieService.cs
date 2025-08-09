using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Services
{
    public class MovieService
    {
        private readonly AzureService _azureService;
        private readonly MovieDbContext _dbContext;
        public MovieService(AzureService azureService, MovieDbContext dbContext)
        {
            _azureService = azureService;
            _dbContext = dbContext;
        }
        public async Task<string> GetMovieBlobUrlAsync(string blobName)
        {
            var url = await _azureService.GetBlobUrlAsync(blobName);
            return url ?? string.Empty;
        }

        public async Task<Movie?> GetMoviesDetailsAsync(int movieId)
        {
            Movie? movie = await _dbContext.Movies
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                return null;

            movie.CoverImage = await GetMovieBlobUrlAsync(movie.CoverImage);
            return movie;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            var movies = await _dbContext.Movies
                .Include(m => m.Ratings)
                .ToListAsync();

            foreach (var movie in movies)
            {
                movie.CoverImage = await GetMovieBlobUrlAsync(movie.CoverImage);
            }
            return movies;
        }

        public async Task<Rating?> AddRatingToMovieAsync(int movieId, int value)
        {
            if (value < 1 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value), "Rating value must be between 1 and 5.");

            Movie? movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
                return null;

            Rating rating = new Rating
            {
                Value = value,
                MovieId = movieId
            };

            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();

            return rating;
        }
    }
}
