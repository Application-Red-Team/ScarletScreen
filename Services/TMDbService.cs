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
            Movie movie = await client.GetMovieAsync(tmdbId, MovieMethods.Videos);


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

            // Process the search results asynchronously
            var tasks = movieResults.Results.Select(movie => MapToTmdbResult(movie));
            var tmdbResults = await Task.WhenAll(tasks);

            return tmdbResults.ToList();
        }
        public async Task<string?> GetUSCertification(int tmdbId)
        {
            TMDbClient client = new TMDbClient(_apiKey);

            // Fetch movie release dates
            ResultContainer<ReleaseDatesContainer> releaseDatesResult = await client.GetMovieReleaseDatesAsync(tmdbId);

            // Extract the US certification using the provided logic
            string? usCertification = ExtractUsCertification(releaseDatesResult);

            return usCertification;
        }

        private static string? ExtractUsCertification(ResultContainer<ReleaseDatesContainer> releaseDatesResult)
        {
            foreach (var result in releaseDatesResult.Results)
            {
                if (result.Iso_3166_1 == "US")
                {
                    foreach (var releaseDate in result.ReleaseDates)
                    {
                        var certification = releaseDate.Certification;
                        if (!string.IsNullOrEmpty(certification))
                        {
                            return certification;
                        }
                    }
                }
            }

            // If us_certification is not found, return a default value
            return null;
        }
        private async Task<TmdbResult> MapToTmdbResult(SearchMovie movie)
        {
            var usCertification = await GetUSCertification(movie.Id);
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
                vote_count = movie.VoteCount,
                us_certification = usCertification
            };
        }
        private async Task<MovieModel> MapToMovieModel(SearchMovie movie)
        {
            var usCertification = await GetUSCertification(movie.Id);

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
                release_date = movie.ReleaseDate,
                us_certification = usCertification
            };
        }
    }
}
