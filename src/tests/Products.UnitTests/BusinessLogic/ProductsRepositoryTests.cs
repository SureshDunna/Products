using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Moq;
using Products.BusinessLogic;
using Products.DataAccess;
using Products.Models;
using Xunit;

namespace Products.UnitTests.BusinessLogic
{
    public class ProductsRepositoryTests
    {
        private readonly Mock<IMongoDbContext> _context;

        private readonly IProductsRepository _productsRepository;

        public ProductsRepositoryTests()
        {
            _context = new Mock<IMongoDbContext>(MockBehavior.Strict);
            _productsRepository = new ProductsRepository(_context.Object);

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

            var data = new HashSet<BsonProduct>();
            data.Add(product1);
            data.Add(product2);
            data.Add(product3);

            _context.Setup(x => x.GetQueryable<BsonProduct>("Products")).Returns(GetProductsMock(data));
        }

        [Fact]
        public async Task can_get_all_products()
        {
            var products = await _productsRepository.GetAllProducts();

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 3);
            Assert.Equal(productsList[0].Id, 1);
            Assert.Equal(productsList[1].Id, 2);
            Assert.Equal(productsList[2].Id, 3);
        }

        [Fact]
        public async Task can_get_all_products_with_minimum_price_range()
        {
            var products = await _productsRepository.GetProductsByPrice(new SearchFilter{ Min = 400 });

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 2);
            Assert.Equal(productsList[0].Id, 2);
            Assert.Equal(productsList[1].Id, 3);
        }

        [Fact]
        public async Task can_get_all_products_with_maximum_price_range()
        {
            var products = await _productsRepository.GetProductsByPrice(new SearchFilter{ Max = 400 });

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 2);
            Assert.Equal(productsList[0].Id, 1);
            Assert.Equal(productsList[1].Id, 2);
        }

        [Fact]
        public async Task can_get_all_products_with_minimum_and_maximum_price_range()
        {
            var products = await _productsRepository.GetProductsByPrice(new SearchFilter{ Min = 450, Max = 500 });

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 1);
            Assert.Equal(productsList[0].Id, 3);
        }

        [Fact]
        public async Task returns_no_products_with_no_minimum_and_no_maximum_price_range()
        {
            var products = await _productsRepository.GetProductsByPrice(new SearchFilter());

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 0);
        }

        [Fact]
        public async Task can_get_all_fantastic_products()
        {
            var products = await _productsRepository.GetFantasticProducts();

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 2);
            Assert.Equal(productsList[0].Id, 1);
            Assert.Equal(productsList[1].Id, 3);
        }

        [Fact]
        public async Task can_get_all_products_with_minimum_rating_range()
        {
            var products = await _productsRepository.GetProductsByRating(new SearchFilter{ Min = 4 });

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 1);
            Assert.Equal(productsList[0].Id, 1);
        }

        [Fact]
        public async Task can_get_all_products_with_maximum_rating_range()
        {
            var products = await _productsRepository.GetProductsByRating(new SearchFilter{ Max = 3 });

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 1);
            Assert.Equal(productsList[0].Id, 2);
        }

        [Fact]
        public async Task can_get_all_products_with_minimum_and_maximum_rating_range()
        {
            var products = await _productsRepository.GetProductsByRating(new SearchFilter{ Min = 3, Max = 4 });

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 1);
            Assert.Equal(productsList[0].Id, 3);
        }

        [Fact]
        public async Task returns_no_products_with_no_minimum_and_no_maximum_rating_range()
        {
            var products = await _productsRepository.GetProductsByRating(new SearchFilter());

            Assert.NotNull(products);

            var productsList = products.ToList();

            Assert.True(productsList.Count == 0);
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