using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace API_AquaSmart.Models
{
    public class SensorHumedad
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = string.Empty;


        [BsonElement("ValorActualHumedad")]
        public int ValorActualHumedad { get; set; }
    }
    public class DatosHumedadDTO
    {
        [Required(ErrorMessage ="El id es requerido")]
        public string IdSensor { get; set; }
        public int ValorActualHumedad { get; set; }
    }

}
