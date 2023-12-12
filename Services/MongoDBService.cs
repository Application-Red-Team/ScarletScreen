using System.Linq;
using MongoDB.Driver;
using ScarletScreen.Model;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;

namespace ScarletScreen.Services
{
    public class MongoDBService
    {
       private readonly IMongoCollection<MovieModel> _movieCollection;
       private readonly TMDbService _tmdbService;

        public MongoDBService(IMongoDatabase database, TMDbService tmdbService)
        {
            _movieCollection = database.GetCollection<MovieModel>("Movies");
            _tmdbService = tmdbService;
        }

        public MovieModel SearchLocalDatabase(string query)
        {
            // Implement logic to search for a movie in the local MongoDB collection
            // Example: Case-insensitive search on the 'Title' field
            var filter = Builders<MovieModel>.Filter.Regex(
                m => m.title,
                new MongoDB.Bson.BsonRegularExpression(query, "i")
            );

            return _movieCollection.Find(filter).FirstOrDefault();
        }

        public void AddDataFromTmdb(TmdbResult tmdbResult)
        {
            // Fetch additional details from TMDb using the movie ID
            Movie movieDetails = _tmdbService.GetMovieDetails(tmdbResult.id);

            // Serialize the data into your MovieModel format
            var movieModel = new MovieModel
            {
                tmdb_id = tmdbResult.id,
                title = tmdbResult.title,
                overview = tmdbResult.overview,
                runtime = tmdbResult.runtime,
                popularity = tmdbResult.popularity,
                us_certification = tmdbResult.us_certification,
                genre_ids = tmdbResult.genre_ids,
                backdrop_path = tmdbResult.backdrop_path,
                poster_path = tmdbResult.poster_path,
                original_language = tmdbResult.original_language,
                vote_average = tmdbResult.vote_average,
                vote_count = tmdbResult.vote_count,
                release_date = tmdbResult.release_date
            };

            // Insert the data into the MongoDB collection
            _movieCollection.InsertOne(movieModel);
        }
    }
}
