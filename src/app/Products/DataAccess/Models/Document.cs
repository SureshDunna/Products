using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.DataAccess
{
    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Common attributes can be represented here like Id etc.
    /// </summary>
    public class Document : IDocument
    {
    }
}