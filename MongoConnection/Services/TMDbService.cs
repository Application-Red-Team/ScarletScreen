using TMDbLib.Client;

namespace ScarletScreen.MongoConnection.Services
{
    public class TMDbService
{
    private readonly TMDbClient _tmdbClient;

    public TMDbService(IConfiguration configuration)
    {
        var apiKey = configuration["TMDb:ApiKey"];
        _tmdbClient = new TMDbClient(apiKey);
    }

    public TMDbClient GetTMDbClient()
    {
        return _tmdbClient;
    }
}

}
