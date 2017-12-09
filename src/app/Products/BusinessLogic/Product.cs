using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Products.DataAccess;

namespace Products.BusinessLogic
{
    /// <summary>
    /// Product document from products collection
    /// </summary>
    public class BsonProduct : Document
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public long Id { get; set; }

        [BsonElement("sku")]
        public string Sku { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("attribute")]
        public BsonAttribute Attribute { get; set; }

    }

    public class BsonAttribute
    {
        [BsonElement("fantastic")]
        public BsonFantastic Fantastic { get; set; }

        [BsonElement("rating")]
        public BsonRating Rating { get; set; }
    }

    public class BsonFantastic
    {
        [BsonElement("value")]
        public bool Value { get; set; }

        [BsonElement("type")]
        public int Type { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }

    public class BsonRating
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("value")]
        public double Value { get; set; }
    }
}