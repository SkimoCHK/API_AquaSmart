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

        [BsonElement("IdHorario")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string refHorarioId { get; set; }

        public HorarioRiego horarioRiego { get; set; }

    }

    public class AreaDTO
    {
        [Required]
        [Length(8, 50, ErrorMessage = "El nombre del area debe estar entre 8 y 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El id de horario es rquerido")]
        public string refHorarioId { get; set; }
    }

}
