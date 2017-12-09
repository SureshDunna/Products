namespace Products.DataAccess
{
    /// <summary>
    /// Config parameters for connecting MongoDb
    /// </summary>
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}