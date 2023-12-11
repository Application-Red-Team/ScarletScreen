﻿namespace ScarletScreen.Model
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class MovieModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int tmdb_id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public int runtime { get; set; }
        public double popularity { get; set; }
        public string us_certification { get; set; }
        public List<int> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public DateTime release_date { get; set; }

        public bool video { get; set; }
    }
}