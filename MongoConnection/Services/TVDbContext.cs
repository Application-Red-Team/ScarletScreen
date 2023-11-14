namespace ScarletScreen.MongoConnection.Services
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using ScarletScreen.MongoConnection.Models;

    public class TVDbContext
{
    private readonly IMongoCollection<TVShow> _tvShowsCollection;

    public TVDbContext(IMongoDatabase database)
    {
        _tvShowsCollection = database.GetCollection<TVShow>("TVShows");
    }

    public TVShow GetTVShowByTitle(string title)
    {
        // Use LINQ to query the MongoDB collection
        return _tvShowsCollection.AsQueryable().FirstOrDefault(tvShow => tvShow.Title.Contains(title));
    }
}
}