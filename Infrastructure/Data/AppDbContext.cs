using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PersonalHealthManager.WebAPI.Models;
using MongoDB.Bson;

namespace PersonalHealthManager.Infrastructure.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _counters;
        public AppDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("Mongoconnection:ConnectionString").Value;
            var databaseName = configuration.GetSection("Mongoconnection:DatabaseName").Value;
            var client = new MongoClient(connectionString);
            try
            {
                _database = client.GetDatabase(databaseName);
            }
            catch (MongoException ex)
            {
                throw new Exception("Error connecting to the database", ex);
            }
            _counters = _database.GetCollection<BsonDocument>("counters");
        }
        public IMongoCollection<ClientsBd> Clients
        {
            get
            {
                try
                {
                    return _database.GetCollection<ClientsBd>("clients");
                }
                catch (MongoException ex)
                {                    
                    throw new Exception("Error getting the clients collection", ex);
                }
            }
        } 
        public async Task SetClients(ClientsBd client)
        {
            var collection = _database.GetCollection<ClientsBd>("clients");
            try
            {
                await collection.InsertOneAsync(client);
            }
            catch (MongoException ex)
            {
                throw new Exception("Error inserting the client", ex);
            }
        }
        public int GetNextClientID()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "clientid");
            var update = Builders<BsonDocument>.Update.Inc("seq", 1);
            var options = new FindOneAndUpdateOptions<BsonDocument>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };
            var document = _counters.FindOneAndUpdate(filter, update, options);
            return document["seq"].AsInt32;
        }
    }
}