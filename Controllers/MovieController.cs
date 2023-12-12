using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ScarletScreen.Model;
using ScarletScreen.Services;
using System;
using System.Threading.Tasks;

namespace ScarletScreen.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMongoCollection<MovieModel> _movieCollection;
        private readonly TMDbService _tmdbService;

        public MovieController(IMongoDatabase database, TMDbService tmdbService)
        {
            _movieCollection = database.GetCollection<MovieModel>("movies");
            _tmdbService = tmdbService;
        }

        public async Task<IActionResult> Details(string tmdb_id)
        {
            Console.WriteLine($"Attempting to retrieve movie with tmdb_id: {tmdb_id}");

            if (!int.TryParse(tmdb_id.Trim(), out var tmdbId))
            {
                Console.WriteLine("Invalid tmdb_id format");
                return BadRequest("Invalid tmdb_id format");
            }

            var filter = Builders<MovieModel>.Filter.Eq(m => m.tmdb_id, tmdbId);
            Console.WriteLine($"MongoDB Query: {filter.Render(BsonSerializer.SerializerRegistry.GetSerializer<MovieModel>(), BsonSerializer.SerializerRegistry)}");

            var movie = _movieCollection.Find(filter).FirstOrDefault();

            if (movie == null)
            {
                Console.WriteLine("Movie not found in the local database. Fetching from TMDb...");

                // Fetch movie details from TMDb using the tmdb_id
                var tmdbResult = await _tmdbService.GetMovieDetails(tmdbId);

                // Fetch US certification for the movie
                var usCertification = await _tmdbService.GetUSCertification(tmdbId);

                // Convert TMDb Movie to MovieModel
                var localMovie = MovieModelConverter.FromTMDbMovie(tmdbResult);

                // Update the local movie with US certification
                localMovie.us_certification = usCertification;

                // Update the local database with TMDb data and US certification
                _movieCollection.InsertOne(localMovie);

                movie = localMovie;
            }
            // Backup in case an id is entered that does not exist
            if (movie == null)
            {
                Console.WriteLine("Movie not found in TMDb either.");
                return NotFound();
            }

            Console.WriteLine($"Movie title: {movie.title}");

            return View("MovieDetails", movie);
        }
    }
}
