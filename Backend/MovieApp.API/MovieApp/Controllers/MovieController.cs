using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Services;

namespace MovieApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly AzureService _azureService;
        private readonly MovieService _movieService;

        public MovieController(AzureService azureService, MovieService movieService)
        {
            _azureService = azureService;
            _movieService = movieService;
        }

        [Authorize]
        [HttpGet("movies/{movieId}")]
        public async Task<IActionResult> GetMovieDetails(int movieId)
        {
            var movie = await _movieService.GetMoviesDetailsAsync(movieId);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }

        [AllowAnonymous]
        [HttpGet("movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [AllowAnonymous]
        [HttpPost("movies/{movieId}/ratings")]
        public async Task<IActionResult> AddRating(int movieId, [FromBody] int value)
        {
            try
            {
                var rating = await _movieService.AddRatingToMovieAsync(movieId, value);
                if (rating == null)
                    return NotFound();
                return CreatedAtAction(nameof(GetMovieDetails), new { movieId }, rating);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
