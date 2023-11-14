namespace ScarletScreen.MongoConnection.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ScarletScreen.MongoConnection.Services;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly MoviesDbContext _moviesDbContext;
        private readonly TVDbContext _tvDbContext;

        public SearchController(MoviesDbContext moviesDbContext, TVDbContext tvDbContext)
        {
            _moviesDbContext = moviesDbContext;
            _tvDbContext = tvDbContext;
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            try
            {
                // Search in movie and TV databases
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
                    return NotFound("No results found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error searching: {ex.Message}");
            }
        }
    }
}
