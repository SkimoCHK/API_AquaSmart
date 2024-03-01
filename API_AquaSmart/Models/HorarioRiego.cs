using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
namespace API_AquaSmart.Models
{
    public class HorarioRiego
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("HoraInicio")]
        public DateTime HoraInicio { get; set; }

        [BsonElement("HoraFin")]
        public DateTime HoraFin { get; set; }


    }
}
