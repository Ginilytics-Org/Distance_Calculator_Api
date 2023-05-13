using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DistanceCalculator.Model.Models
{
    public class CityModel
    {
        public int ZIP { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }
        public string City { get; set; }
    }
    public class ZipCordinates
    {
        public int LAT { get; set; }
        public int LNG { get; set; }
    }
}
