using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

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
    public class ElectroValvulaDTO
    {
        [Required(ErrorMessage ="El campo del nombre es obligatorio")]
        
        public string Nombre { set; get; } = string.Empty;
        public bool Abierta { get; set; }
    }
    public class ValvulaRequest
    {
        [Required(ErrorMessage = "El id es requerido")]
        public string id { get; set; } = string.Empty;
        public bool Status { get; set; }
    }

}
