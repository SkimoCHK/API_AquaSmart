using API_AquaSmart.Services;
using API_AquaSmart.Controllers;
using API_AquaSmart.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using API_AquaSmart.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;


namespace API_AquaSmart.Services
{
    public class HorarioRiegoServices
    {
        private readonly IMongoCollection<HorarioRiego> _horarioRiegoCollection;
        public HorarioRiegoServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var client =  new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            _horarioRiegoCollection = database.GetCollection<HorarioRiego>(databaseSettings.Value.Collections["HorarioRiego"]);
        }

        public async Task<List<HorarioRiego>> GetHorarios()
        {
            return await _horarioRiegoCollection.Find(c => true).ToListAsync();
        }

        public async Task<HorarioRiego> GetHorarioById(string ID)
        {
             return await _horarioRiegoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(ID) } }).Result.FirstAsync();
        }
        
        public async Task InsertHorario(HorarioRiego horario)
        {
          await  _horarioRiegoCollection.InsertOneAsync(horario);
        }

        public async Task UpdateHorario(HorarioRiego horario)
        {
            var filter = Builders<HorarioRiego>.Filter.Eq(c => c.Id, horario.Id);
            await _horarioRiegoCollection.ReplaceOneAsync(filter, horario);
        }


        public async Task DeleteHorario(string id)
        {
            var filter = Builders<HorarioRiego>.Filter.Eq(s => s.Id, id);
            await _horarioRiegoCollection.DeleteOneAsync(filter);
        }


    }
}
