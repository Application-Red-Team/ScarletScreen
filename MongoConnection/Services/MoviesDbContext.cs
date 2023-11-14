namespace ScarletScreen.MongoConnection.Services 
{ 
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using ScarletScreen.MongoConnection.Models;

    public class MoviesDbContext
    {
        private readonly IMongoDatabase _database;

        public MoviesDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.MoviesDatabaseName);
        }

        public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("movies");
        // Add other collections as needed
}
}