namespace ScarletScreen.MongoConnection.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class TVShow
    {
        [BsonId]
        public int TMDbId { get; set; } // Using TMDb ID as the primary identifier

        public string Name { get; set; }

        public string Overview { get; set; }

        public DateTime? FirstAirDate { get; set; } // Nullable DateTime

        public List<string> Genres { get; set; }

        public string PosterPath { get; set; }

        // Add the ContentRatings property for storing content rating information
        public List<ContentRating> ContentRatings { get; set; }

        // Grabbing only the US Content Rating
        /*public void SetUSContentRating(List<ContentRating> contentRatings)
        {
            var usRating = contentRatings.FirstOrDefault(r => r.Iso31661 == "US");
            USContentRating = usRating?.Rating ?? "N/A";
        }*/
    }

    public class ContentRating
    {
        public string Country { get; set; }

        public string Rating { get; set; }
    }
}
