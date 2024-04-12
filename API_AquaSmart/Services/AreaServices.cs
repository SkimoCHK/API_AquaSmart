using API_AquaSmart.Services;
using API_AquaSmart.Controllers;
using API_AquaSmart.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using API_AquaSmart.Configuration;

namespace API_AquaSmart.Services
{
    public class AreaServices
    {
        private readonly IMongoCollection<Area> _areasCollection;
        

        public AreaServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            _areasCollection = database.GetCollection<Area>(databaseSettings.Value.Collections["Areas"]);

        }
        public async Task<List<Area>> getAsync()
        {
            return  await _areasCollection.Find(c => true).ToListAsync();

        }
        public async Task<Area> GetAreaById(string ID)
        {
            ObjectId objectId;

            // Intentamos convertir la cadena de ID en un ObjectId
            if (!ObjectId.TryParse(ID, out objectId))
            {
                // Si la conversión falla, lanzamos una excepción
                throw new ArgumentException("El ID proporcionado no tiene el formato correcto.");
            }

            // Utilizamos el ObjectId convertido para buscar el área
            return await _areasCollection.Find(new BsonDocument { { "_id", objectId } }).FirstOrDefaultAsync();
        }



        public async Task InsertArea(Area area)
        {
                await _areasCollection.InsertOneAsync(area);
        }


        public async Task UpdateArea(Area area)
        {
            var filter = Builders<Area>.Filter.Eq(s => s.id, area.id);
            await _areasCollection.ReplaceOneAsync(filter, area);
        }

        public async Task DeleteArea(string ID)
        {
            var filter = Builders<Area>.Filter.Eq(s => s.id, ID);
            await _areasCollection.DeleteOneAsync(filter);
        }

    }
}
