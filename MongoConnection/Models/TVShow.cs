namespace ScarletScreen.MongoConnection.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class TVShow
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("BackdropPath")]
        public string BackdropPath { get; set; }

        [BsonElement("GenreIds")]
        public List<int> GenreIds { get; set; }

        [BsonElement("Overview")]
        public string Overview { get; set; }

        [BsonElement("Popularity")]
        public double Popularity { get; set; }

        [BsonElement("PosterPath")]
        public string PosterPath { get; set; }

        [BsonElement("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        [BsonElement("USContentRating")]
        public string USContentRating { get; set; }

        // Grabbing only the US Content Rating
        public void SetUSContentRating(List<ContentRating> contentRatings)
        {
            var usRating = contentRatings.FirstOrDefault(r => r.Iso31661 == "US");
            USContentRating = usRating?.Rating ?? "N/A";
        }
    }

    public class ContentRating
    {
        [BsonElement("iso_3166_1")]
        public string Iso31661 { get; set; }

        [BsonElement("rating")]
        public string Rating { get; set; }
    }
}
