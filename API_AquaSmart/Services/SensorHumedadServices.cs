using API_AquaSmart.Configuration;
using API_AquaSmart.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_AquaSmart.Services
{
    public class SensorHumedadServices
    {
        private readonly IMongoCollection<SensorHumedad> _sensoresHumedadCollection;

        public SensorHumedadServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            _sensoresHumedadCollection = database.GetCollection<SensorHumedad>(databaseSettings.Value.Collections["SensoresHumedad"]);
        }

        public async Task<List<SensorHumedad>> GetSensoresHumedadAsync()
        {
            return await _sensoresHumedadCollection.Find(c => true).ToListAsync();
        }

        public async Task<SensorHumedad> GetSensorHumedadById(string id)
        {
            return await _sensoresHumedadCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task InsertSensorHumedad(SensorHumedad sensorHumedad)
        {
            await _sensoresHumedadCollection.InsertOneAsync(sensorHumedad);
        }

        public async Task UpdateSensorHumedad(SensorHumedad sensorHumedad)
        {
            var filter = Builders<SensorHumedad>.Filter.Eq(s => s.Id, sensorHumedad.Id);
            await _sensoresHumedadCollection.ReplaceOneAsync(filter, sensorHumedad);
        }

        public async Task DeleteSensorHumedad(string id)
        {
            var filter = Builders<SensorHumedad>.Filter.Eq(s => s.Id, id);
            await _sensoresHumedadCollection.DeleteOneAsync(filter);
        }
    }
}
