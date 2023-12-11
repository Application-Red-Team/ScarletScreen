using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ScarletScreen.Model;
using System;
using TMDbLib.Objects.Movies;

namespace ScarletScreen.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMongoCollection<MovieModel> _movieCollection;

        public MovieController(IMongoDatabase database)
        {
            _movieCollection = database.GetCollection<MovieModel>("movies");
        }

        public IActionResult Details(string tmdb_id)
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
                Console.WriteLine("Movie not found!");
                return NotFound();
            }

            Console.WriteLine($"Movie title: {movie.title}");
                    // Add more debug lines as needed

            return View(movie);
        }
    }
}
