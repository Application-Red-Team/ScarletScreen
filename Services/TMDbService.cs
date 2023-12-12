using MySqlX.XDevAPI;
using ScarletScreen.Model;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Trending;


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
        public async Task<List<int>> GetTrendingMovieIds()
        {
            // Initialize the TMDb client
            TMDbClient client = new TMDbClient(_apiKey);
             // Fetch trending movies
            var trendingMovies = await client.GetTrendingMoviesAsync(TimeWindow.Week);

            // Return only the TMDB IDs
            return trendingMovies.Results.Select(movie => movie.Id).ToList();
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
        private MovieModel MapToMovieModel(SearchMovie movie)
        {
            return new MovieModel
            {
                tmdb_id = movie.Id,
                title = movie.Title,
                overview = movie.Overview,
                popularity = movie.Popularity,
                genres = movie.GenreIds.Select(id => new Genre { Id = id }).ToList(),
                backdrop_path = movie.BackdropPath,
                poster_path = movie.PosterPath,
                original_language = movie.OriginalLanguage,
                vote_average = movie.VoteAverage,
                vote_count = movie.VoteCount,
                release_date = movie.ReleaseDate
            };
        }

    }
}
