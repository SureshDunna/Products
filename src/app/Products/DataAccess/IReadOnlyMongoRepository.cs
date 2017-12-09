using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Products.DataAccess
{
    /// <summary>
    /// defines all the methods to read the documents from Mongo Db
    /// </summary>
    public interface IReadOnlyMongoRepository
    {
        Task<List<TDocument>> GetAllAsync<TDocument>(string collectionName) where TDocument : IDocument;
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string collectionName) where TDocument : IDocument;
        IMongoQueryable<TDocument> GetQueryable<TDocument>(string collectionName) where TDocument : IDocument;
    }
}