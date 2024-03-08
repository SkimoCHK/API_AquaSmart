﻿using MongoDB.Bson.Serialization.Attributes;

namespace API_AquaSmart.Models
{
    public class RiegoEvent
    {
        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; }

        [BsonElement("Duracion")]
        public int Duracion { get; set; }
    }
}
