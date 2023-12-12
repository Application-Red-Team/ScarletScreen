namespace ScarletScreen.Model
{
    public class MovieModelConverter
    {
        public static MovieModel FromTMDbMovie(TMDbLib.Objects.Movies.Movie tmdbMovie)
        {
            string videoKey = null;

            // Attempting to find Trailer
            var trailer = tmdbMovie.Videos.Results.FirstOrDefault(video => video.Type == "Trailer");

            // Check if a trailer is found
            if (trailer != null)
            {
                videoKey = trailer.Key;
                // Now you have the video key for the trailer
                Console.WriteLine($"Trailer Video Key: {videoKey}");
            }

            // Convert TMDb Movie to MovieModel
            return new MovieModel
            {
                tmdb_id = tmdbMovie.Id,
                title = tmdbMovie.Title,
                overview = tmdbMovie.Overview,
                runtime = tmdbMovie.Runtime,
                popularity = tmdbMovie.Popularity,
                genres = tmdbMovie.Genres,
                backdrop_path = tmdbMovie.BackdropPath,
                poster_path = tmdbMovie.PosterPath,
                original_language = tmdbMovie.OriginalLanguage,
                vote_average = tmdbMovie.VoteAverage,
                vote_count = tmdbMovie.VoteCount,
                release_date = tmdbMovie.ReleaseDate,
                WatchProviders = tmdbMovie.WatchProviders,
                VideoKey = videoKey
            };
        }
    }
}
