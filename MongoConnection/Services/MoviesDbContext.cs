namespace ScarletScreen.MongoConnection.Services 
{
    using ScarletScreen.MongoConnection.Models;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    public class MoviesDbContext
    {
        private readonly IMongoCollection<Movie> _moviesCollection;

        public MoviesDbContext(IMongoDatabase database)
        {
            _moviesCollection = database.GetCollection<Movie>("Movies");
        }

        public Movie GetMovieByTitle(string title)
        {
            // Use LINQ to query the MongoDB collection
            return _moviesCollection.AsQueryable().FirstOrDefault(movie => movie.Title.Contains(title));
        }
    }
}