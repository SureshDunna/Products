using MongoDB.Driver;

namespace Products.DataAccess
{
    public interface IMongoDbHealthCheck
    {
        bool IsHealthy { get; }
    }

    /// <summary>
    /// Claas used for doing the health check of mongo db
    /// </summary>
    public class MongoDbHealthCheck : IMongoDbHealthCheck
    {
        public readonly IMongoDbContext _context;

        /// <summary>
        /// this can be improved to check the cluster status or servers statuses
        /// </summary>
        public bool IsHealthy => _context.Client != null && _context.Database != null;

        public MongoDbHealthCheck(IMongoDbContext context)
        {
            _context = context;
        }
    }
}