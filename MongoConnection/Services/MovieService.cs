namespace ScarletScreen.MongoConnection.Services
{
    using ScarletScreen.MongoConnection.Models;
    using System.Threading.Tasks;

public class MovieService
{
    private readonly MoviesDbContext _mongoDbContext;

    public MovieService(MoviesDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task InsertMovieAsync(Movie movie)
    {
        await _mongoDbContext.Movies.InsertOneAsync(movie);
    }

    // Add other movie-related methods as needed
}
}
