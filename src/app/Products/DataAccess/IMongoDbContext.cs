using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Products.DataAccess
{
    /// <summary>
    /// Defines the context of the mongo db
    /// </summary>
    public interface IMongoDbContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
        IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName);
        IMongoQueryable<TDocument> GetQueryable<TDocument>(string collectionName);
    }
}