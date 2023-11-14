namespace ScarletScreen.MongoConnection.Services
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using ScarletScreen.MongoConnection.Models;

    public class TVDbContext
    {
        private readonly IMongoCollection<TVShow> _tvShowsCollection;
        private readonly TMDbService _tmdbService;

        public TVDbContext(IMongoDatabase database, TMDbService tmdbService)
        {
            _tvShowsCollection = database.GetCollection<TVShow>("TVShows");
            _tmdbService = tmdbService;
        }

        public TVShow GetTVShowByTitle(string title)
        {
            // Renamed Name Field on TVShow.cs to Title for parity with movie method
            return _tvShowsCollection.AsQueryable().FirstOrDefault(tvShow => tvShow.Name.Contains(title));
        }

        public async Task AddTVShow(TVShow tvShow)
        {
            var tmdbClient = _tmdbService.GetTMDbClient();
        
            // Use tmdbClient to fetch detailed information about the TV show
            var tmdbTVShowDetails = await tmdbClient.GetTvShowAsync(tvShow.TMDbId);

            // Extract content rating information
            var contentRatings = tmdbTVShowDetails.ContentRatings;

            /*
                TODO!!!
                Process Content Ratings
            */
        }
    }
}