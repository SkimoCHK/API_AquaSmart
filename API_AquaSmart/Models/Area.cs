using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_AquaSmart.Models
{
    public class Area
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string id { get; set; } = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("Imagen")]
        public string Imagen { get; set; } = string.Empty;

        [BsonElement("IdSensor")]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string IdSensor { get; set; } = string.Empty;
        public SensorHumedad SensorHumedad { get; set; }

        [BsonElement("IdValvula")]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string IdValvula { get; set; } = string.Empty;
        public ElectroValvula valvula { get; set; }

        
        [BsonElement("HistorialRiego")]
        public List<RiegoEvent> HistorialRiego { get; set; } = new List<RiegoEvent>();


    }

    public class ActivarRiegoRequest
    {
        public string AreaId { get; set; }
        public int Duracion { get; set; }
    }


  

    public class AreaDTO
    {
        [Required(ErrorMessage = "El nombre del area es requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es requerida una imagen")]
        public string Imagen { get; set; } = string.Empty;

        [Required(ErrorMessage = "El id del sensor es rquerido")]
        public string refSensor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El id de la valvula es rquerido")]
        public string refValvula { get; set; } = string.Empty;
    }

    public class AreaUpdateDTO
    {
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La longitud del nombre del área debe estar entre 8 y 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
   
    }

   


}
