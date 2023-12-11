namespace ScarletScreen.Model
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class TVModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int tmdb_id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public int number_of_seasons { get; set; }
        public int number_of_episodes { get; set; }
        public double popularity { get; set; }
        public string us_certification { get; set; }
        //[BsonIgnore] // Ignoring genres for now
        //public List<int> genres { get; set; }
        public string backdrop_path { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        //public double vote_average { get; set; }
        //public int vote_count { get; set; }
        //public DateTime release_date { get; set; }
    }
}