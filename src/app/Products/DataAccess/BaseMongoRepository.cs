namespace Products.DataAccess
{
    /// <summary>
    /// This Class can be used for all CRUD operations operated on MongoDb
    /// </summary>
    public abstract class BaseMongoRepository : ReadOnlyMongoRepository, IBaseMongoRepository
    {
        protected BaseMongoRepository(IMongoDbContext context)
        {
            MongoDbContext = context;
        }
    }
}