using MongoDB.Bson.Serialization.Attributes;
using TMDbLib.Objects.General;

namespace ScarletScreen.Model
{
    public class SearchViewModel
    {
        public List<TmdbResult> TmdbResults { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class TmdbResult
    {
        public string backdrop_path { get; set; }
        public List<Genre> genres { get; set; }
        public int id { get; set; }
        public int? runtime {  get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public DateTime? release_date { get; set; }
        public string title { get; set; }
        public string? us_certification { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}