using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace API_AquaSmart.Models
{
    public class ElectroValvula
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("Status")]
        public bool Abierta { get; set; } 

    }

}
