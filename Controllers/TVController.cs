using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ScarletScreen.Model;
using System;
using TMDbLib.Objects.Movies;

namespace ScarletScreen.Controllers
{
    public class TVController : Controller
    {
        private readonly IMongoCollection<TVModel> _tvCollection;

        public TVController(IMongoDatabase database)
        {
            _tvCollection = database.GetCollection<TVModel>("tv_shows");
        }

        public IActionResult Details(string tmdb_id)
        {
            Console.WriteLine($"Attempting to retrieve show with tmdb_id: {tmdb_id}");

            if (!int.TryParse(tmdb_id.Trim(), out var tmdbId))
            {
                Console.WriteLine("Invalid tmdb_id format");
                return BadRequest("Invalid tmdb_id format");
            }

            var filter = Builders<TVModel>.Filter.Eq(m => m.tmdb_id, tmdbId);
            Console.WriteLine($"MongoDB Query: {filter.Render(BsonSerializer.SerializerRegistry.GetSerializer<TVModel>(), BsonSerializer.SerializerRegistry)}");

            var show = _tvCollection.Find(filter).FirstOrDefault();

            if (show == null)
            {
                Console.WriteLine("TV Show not found!");
                return NotFound();
            }

            Console.WriteLine($"TV Show Name: {show.name}");
                    // Add more debug lines as needed

            return View("ShowDetails", show);
        }
    }
}
