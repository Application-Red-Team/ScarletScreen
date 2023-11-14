namespace ScarletScreen.MongoConnection.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class Movie
    {
        [BsonId]
        public int TMDbId { get; set; } // Using TMDb ID as the primary identifier

        public string Title { get; set; }

        public string Overview { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? Runtime { get; set; }

        public string USContentRating { get; set; }

        public List<string> Genres { get; set; }

        public string PosterPath { get; set; }

        // ... other properties as needed
    }
}