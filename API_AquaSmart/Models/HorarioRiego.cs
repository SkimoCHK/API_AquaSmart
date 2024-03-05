using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Text.Json.Serialization;
namespace API_AquaSmart.Models
{
    public class HorarioRiego
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; } = string.Empty;

        [BsonElement("HoraInicio")]
        public DateTime HoraInicio { get; set; }

        [BsonElement("HoraFin")]
        public DateTime HoraFin { get; set; }


    }
}
