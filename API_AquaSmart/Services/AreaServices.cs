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
        private readonly IMongoCollection<HorarioRiego> _horarioRiego;

        public AreaServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            _areasCollection = database.GetCollection<Area>(databaseSettings.Value.Collections["Areas"]);
            _horarioRiego = database.GetCollection<HorarioRiego>(databaseSettings.Value.Collections["HorarioRiego"]);
        }
        public async Task<List<Area>> getAsync()
        {
            return await _areasCollection.Find(c => true).ToListAsync();
        }
        public async Task<Area> GetAreaById(string ID)
        {
            var area = await _areasCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(ID) } }).Result.FirstAsync();
            var riego = await _horarioRiego.FindAsync(new BsonDocument { { "_id", new ObjectId(area.refHorarioId) } }).Result.FirstAsync();
            area.horarioRiego = riego;
            return area;
        }

        public async Task InsertArea(AreaDTO area)
        {
            Area newArea = new ()
            {
                Nombre = area.Nombre,
                refHorarioId = area.refHorarioId
            };
            await _areasCollection.InsertOneAsync(newArea);
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
