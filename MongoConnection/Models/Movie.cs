namespace ScarletScreen.MongoConnection.Models
    {
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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

        public void SetUSContentRating(List<ReleaseDateData> releaseDates)
        {
            var usRelease = releaseDates.FirstOrDefault(r => r.Iso31661 == "US");
            USContentRating = usRelease?.ReleaseDates.FirstOrDefault()?.Certification ?? "N/A";
        }
    }

    public class ReleaseDateData
    {
        [BsonElement("iso_3166_1")]
        public string Iso31661 { get; set; }

        [BsonElement("release_dates")]
        public List<ReleaseData> ReleaseDates { get; set; }
    }

    public class ReleaseData
    {
        [BsonElement("certification")]
        public string Certification { get; set; }

        [BsonElement("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}