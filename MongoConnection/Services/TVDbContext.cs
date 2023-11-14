namespace ScarletScreen.MongoConnection.Services 
{ 
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using ScarletScreen.MongoConnection.Models;

    public class TVDbContext
    {
        private readonly IMongoDatabase _database;

        public TVDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.TVDatabaseName);
        }

        public IMongoCollection<TVShow> TVShow => _database.GetCollection<TVShow>("shows");
        // Add other collections as needed
}
}