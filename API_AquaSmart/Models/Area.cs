using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_AquaSmart.Models
{
    public class Area
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string id = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = string.Empty;


    }
    
}
