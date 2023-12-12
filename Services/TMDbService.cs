using ScarletScreen.Model;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;


namespace ScarletScreen.Services
{
    public class TMDbService
    {
        private readonly string _apiKey;

        public TMDbService(IConfiguration configuration)
        {
            _apiKey = configuration["TMDbSettings:ApiKey"];
        }

        public async Task<Movie> GetMovieDetails(int tmdbId)
        {
            // Initialize the TMDb client
            TMDbClient client = new TMDbClient(_apiKey);

            // Fetch movie details using the TMDb ID
            Movie movie = await client.GetMovieAsync(tmdbId);

            return movie;
        }
        public async Task<List<TmdbResult>> SearchTmdb(string query)
        {
            // Initialize the TMDb client
            TMDbClient client = new TMDbClient(_apiKey);

            // Perform the search
            SearchContainer<SearchMovie> movieResults = await client.SearchMovieAsync(query);

            // Process the search results
            List<TmdbResult> tmdbResults = movieResults.Results
                .Select(movie => MapToTmdbResult(movie))
                .ToList();

            return tmdbResults;
        }

        private TmdbResult MapToTmdbResult(SearchMovie movie)
        {
            // Map TMDb search result to TmdbResult model defined in SearchModel
            return new TmdbResult
            {
                backdrop_path = movie.BackdropPath,
                id = movie.Id,
                original_language = movie.OriginalLanguage,
                original_title = movie.OriginalTitle,
                overview = movie.Overview,
                popularity = movie.Popularity,
                poster_path = movie.PosterPath,
                release_date = movie.ReleaseDate,
                title = movie.Title,
                vote_average = movie.VoteAverage,
                vote_count = movie.VoteCount
            };
        }
    }
}
