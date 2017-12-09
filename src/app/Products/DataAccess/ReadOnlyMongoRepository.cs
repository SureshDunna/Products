using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Products.DataAccess
{
    public abstract class ReadOnlyMongoRepository : IReadOnlyMongoRepository
    {
        /// <summary>
        /// The connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The MongoDbContext
        /// </summary>
        protected IMongoDbContext MongoDbContext = null;

        protected ReadOnlyMongoRepository() {}

        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected ReadOnlyMongoRepository(string connectionString, string databaseName)
        {
            MongoDbContext = new MongoDbContext(connectionString, databaseName);
        }

        public Task<List<TDocument>> GetAllAsync<TDocument>(string collectionName) where TDocument : IDocument
        {
            return MongoDbContext.GetQueryable<TDocument>(collectionName).Where(_ => true).ToListAsync();
        }

        public Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string collectionName) where TDocument : IDocument
        {
            return MongoDbContext.GetQueryable<TDocument>(collectionName).Where(filter).ToListAsync();
        }
        
        public IMongoQueryable<TDocument> GetQueryable<TDocument>(string collectionName) where TDocument : IDocument
        {
            return MongoDbContext.GetQueryable<TDocument>(collectionName);
        }
    }
}