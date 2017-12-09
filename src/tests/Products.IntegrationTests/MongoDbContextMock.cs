using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Moq;
using Products.BusinessLogic;
using Products.DataAccess;

namespace Products.IntegrationTests
{
    public static class MongoDbContextMock
    {
        private static Mock<IMongoDbContext> _context;

        static MongoDbContextMock()
        {
            _context = new Mock<IMongoDbContext>();

            _context.Setup(x => x.GetQueryable<BsonProduct>("Products")).Returns(GetProductsMock(GetTestData()));
        }

        public static IMongoDbContext MongoDbContext => _context.Object;

        private static HashSet<BsonProduct> GetTestData()
        {
            var product1 = new BsonProduct 
            { 
                Id = 1, 
                Price = 300, 
                Attribute = new BsonAttribute
                {
                    Fantastic = new BsonFantastic
                    {
                        Value = true
                    },
                    Rating = new BsonRating
                    {
                        Value = 5
                    }
                }
            };
            var product2 = new BsonProduct 
            { 
                Id = 2, 
                Price = 400, 
                Attribute = new BsonAttribute
                {
                    Fantastic = new BsonFantastic
                    {
                        Value = false
                    },
                    Rating = new BsonRating
                    {
                        Value = 2
                    }
                }
            };
            var product3 = new BsonProduct 
            { 
                Id = 3, 
                Price = 500, 
                Attribute = new BsonAttribute
                {
                    Fantastic = new BsonFantastic
                    {
                        Value = true
                    },
                    Rating = new BsonRating
                    {
                        Value = 3.5
                    }
                }
            };

            var testData = new HashSet<BsonProduct>();
            testData.Add(product1);
            testData.Add(product2);
            testData.Add(product3);

            return testData;
        }

        private static IMongoQueryable<BsonProduct> GetProductsMock(HashSet<BsonProduct> data)
        {
            var queryableList = data.AsQueryable();
            var mongoQueryableMock = new Mock<IMongoQueryable<BsonProduct>>();
        
            mongoQueryableMock.As<IQueryable<BsonProduct>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            mongoQueryableMock.As<IQueryable<BsonProduct>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            mongoQueryableMock.As<IQueryable<BsonProduct>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            mongoQueryableMock.As<IQueryable<BsonProduct>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());    

            return mongoQueryableMock.Object;
        }
    }
}