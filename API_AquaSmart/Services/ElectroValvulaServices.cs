using API_AquaSmart.Configuration;
using API_AquaSmart.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API_AquaSmart.Services
{
    public class ElectroValvulaServices
    {
        private readonly IMongoCollection<ElectroValvula> _valvulasCollection;

        public ElectroValvulaServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var cliente = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = cliente.GetDatabase(databaseSettings.Value.DatabaseName);
            _valvulasCollection = database.GetCollection<ElectroValvula>(databaseSettings.Value.Collections["ElectroValvulas"]);
        }

        public async Task<List<ElectroValvula>> GetValvulas()
        {
            return await _valvulasCollection.Find(v => true).ToListAsync();
        }

        public async Task<ElectroValvula> GetValvulaById(string ID)
        {
            return await _valvulasCollection.FindAsync(new BsonDocument { {"_id", new ObjectId(ID)} }).Result.FirstAsync();
        }


        public async Task InsertValvula(ElectroValvula valvula)
        {
            await _valvulasCollection.InsertOneAsync(valvula);
        }

        public async Task UpdateValvula(ElectroValvula valvula)
        {
            var filter = Builders<ElectroValvula>.Filter.Eq(v => v.Id, valvula.Id);
            await _valvulasCollection.ReplaceOneAsync(filter, valvula);
        }

        public async Task DeleteValvula(string id)
        {
            var filter = Builders<ElectroValvula>.Filter.Eq(v => v.Id, id);
            await _valvulasCollection.DeleteOneAsync(filter);
        }


    }
}
