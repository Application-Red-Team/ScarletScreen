namespace ScarletScreen.MongoConnection.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ScarletScreen.MongoConnection.Models;
    using ScarletScreen.MongoConnection.Services;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using TMDbLib.Client;

    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly MoviesDbContext _moviesDbContext;
        private readonly TVDbContext _tvDbContext;
        private readonly TMDbService _tmdbService;

        public SearchController(MoviesDbContext moviesDbContext, TVDbContext tvDbContext, IConfiguration configuration, TMDbService tmdbService)
        {
            _moviesDbContext = moviesDbContext;
            _tvDbContext = tvDbContext;

            _tmdbService = tmdbService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            try
            {
                // Search in your movie and TV databases
                var movieResult = _moviesDbContext.GetMovieByTitle(query);
                var tvResult = _tvDbContext.GetTVShowByTitle(query);

                if (movieResult != null)
                {
                    return Ok(new { Type = "Movie", Result = movieResult });
                }
                else if (tvResult != null)
                {
                    return Ok(new { Type = "TVShow", Result = tvResult });
                }
                else
                {
                    // If not found, query TMDb
                    var tmdbResult = await SearchTMDb(query);

                    if (tmdbResult != null)
                    {
                        // Determine if it's a movie or TV show from TMDb
                        if (tmdbResult is Movie tmdbMovie)
                        {
                            // Add the movie to the local Movies database
                            _moviesDbContext.AddMovie(tmdbMovie);                            

                            return Ok(new { Type = "Movie", Result = tmdbMovie });
                        }
                        else if (tmdbResult is TVShow tmdbTVShow)
                        {
                            // Add the TV show to the local TVShows database
                            _tvDbContext.AddTVShow(tmdbTVShow);

                            return Ok(new { Type = "TVShow", Result = tmdbTVShow });
                        }
                    }
            
                    return NotFound("No results found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error searching: {ex.Message}");
            }
        }

        private async Task<object> SearchTMDb(string query)
        {
            var tmdbClient = _tmdbService.GetTMDbClient();
            // Use TMDbLib to query TMDb API
            var searchResults = await tmdbClient.SearchMovieAsync(query);

            // For simplicity, just return the first result
            var firstResult = searchResults?.Results?.FirstOrDefault();

            return firstResult;
        }
    }
}
