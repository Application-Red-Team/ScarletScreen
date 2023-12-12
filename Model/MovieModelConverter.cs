namespace ScarletScreen.Model
{
    public class MovieModelConverter
    {
        public static MovieModel FromTMDbMovie(TMDbLib.Objects.Movies.Movie tmdbMovie)
        {
            // Convert TMDb Movie to MovieModel
            return new MovieModel
            {
                // Map properties accordingly
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
                release_date = tmdbMovie.ReleaseDate
            };
        }
    }
}
