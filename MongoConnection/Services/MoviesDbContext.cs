namespace ScarletScreen.MongoConnection.Services
{
    using MongoDB.Driver;
    using ScarletScreen.MongoConnection.Models;
    using TMDbLib.Client;

    public class MoviesDbContext
    {
        private readonly IMongoCollection<Movie> _moviesCollection;
        private readonly TMDbService _tmdbService;

        public MoviesDbContext(IMongoDatabase database, TMDbService tmdbService)
        {
            _moviesCollection = database.GetCollection<Movie>("Movies");
            _tmdbService = tmdbService;
        }

        public Movie GetMovieByTitle(string title)
        {
            return _moviesCollection.AsQueryable().FirstOrDefault(movie => movie.Title.Contains(title));
        }

        public async Task AddMovie(Movie movie)
        {
            var tmdbClient = _tmdbService.GetTMDbClient();
            // Fetch detailed information about the movie using TMDbLib
            var tmdbMovieDetails = await tmdbClient.GetMovieAsync(movie.TMDbId);

            // Extract relevant information and update local Movie entity
            movie.Title = tmdbMovieDetails.Title;
            movie.Overview = tmdbMovieDetails.Overview;
            movie.ReleaseDate = tmdbMovieDetails.ReleaseDate;
            movie.Runtime = tmdbMovieDetails.Runtime; // Theatrical runtime in minutes

            // Extract release information
            var releaseDates = tmdbMovieDetails.ReleaseDates?.Results;

            /*
            TODO!!!!! 
            Process ReleaseDates Information here
             */

            // Extract genres
            movie.Genres = tmdbMovieDetails.Genres?.Select(genre => genre.Name).ToList();

            // Extract poster path
            movie.PosterPath = tmdbMovieDetails.PosterPath;

            // Add the updated Movie entity to the local MongoDB collection
            await _moviesCollection.InsertOneAsync(movie);
        }

    }
}