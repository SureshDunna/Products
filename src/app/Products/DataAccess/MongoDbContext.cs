using System;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Products.DataAccess
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        /// <summary>
        /// The constructor of the MongoDbContext, it needs a connection string and a database name.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public MongoDbContext(MongoDbConfig config, ILogger<MongoDbContext> logger)
        {
            try
            {
                Client = new MongoClient(config.ConnectionString);
                Database = Client.GetDatabase(config.Database);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error occured while connecting to mongo db with {config.ConnectionString} and {config.Database}. Error is {ex}");
            }
        }

        /// <summary>
        /// The constructor of the MongoDbContext, it needs a connection string and a database name. 
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        public MongoDbContext(string connectionString, string databaseName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
        {
            return Database.GetCollection<TDocument>(collectionName);
        }

        public IMongoQueryable<TDocument> GetQueryable<TDocument>(string collectionName)
        {
            return Database.GetCollection<TDocument>(collectionName).AsQueryable();
        }
    }
}