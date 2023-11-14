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
        return _tvShowsCollection.AsQueryable().FirstOrDefault(tvShow => tvShow.Title.Contains(title));
    }

    public async Task AddTVShow(TVShow tvShow)
    {
        await _tvShowsCollection.InsertOneAsync(tvShow);
    }
}
}