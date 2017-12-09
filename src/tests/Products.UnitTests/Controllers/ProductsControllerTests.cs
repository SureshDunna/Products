using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Products.BusinessLogic;
using Products.Controllers;
using Products.Models;
using Xunit;

namespace Products.UnitTests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsRepository> _productsRepository;

        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            _productsRepository = new Mock<IProductsRepository>(MockBehavior.Strict);
            _productsController = new ProductsController(_productsRepository.Object);
        }

        [Fact]
        public async Task can_get_all_products()
        {
            _productsRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(
                new List<Product>
                {
                    new Product { Id = 1 },
                    new Product { Id = 2 }
                });
            
            var actionResultResponse = await _productsController.GetAllProducts();

            var response = Assert.IsType<OkObjectResult>(actionResultResponse);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(response.Value);

            Assert.NotNull(products);

            var productsList  = products.ToList();
            Assert.Equal(productsList.Count, 2);
            Assert.Equal(productsList[0].Id, 1);
            Assert.Equal(productsList[1].Id, 2);

            _productsRepository.VerifyAll();
        }

        [Fact]
        public async Task can_get_all_products_by_price()
        {
            _productsRepository.Setup(x => x.GetProductsByPrice(It.IsAny<SearchFilter>())).ReturnsAsync(
                new List<Product>
                {
                    new Product { Id = 1 },
                    new Product { Id = 2 }
                });
            
            var actionResultResponse = await _productsController.GetProductsByPrice(null);

            var response = Assert.IsType<OkObjectResult>(actionResultResponse);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(response.Value);

            Assert.NotNull(products);

            var productsList  = products.ToList();
            Assert.Equal(productsList.Count, 2);
            Assert.Equal(productsList[0].Id, 1);
            Assert.Equal(productsList[1].Id, 2);

            _productsRepository.VerifyAll();
        }

        [Fact]
        public async Task can_get_all_fantastic_products()
        {
            _productsRepository.Setup(x => x.GetFantasticProducts()).ReturnsAsync(
                new List<Product>
                {
                    new Product { Id = 1 },
                    new Product { Id = 2 }
                });
            
            var actionResultResponse = await _productsController.GetFantasticProducts();

            var response = Assert.IsType<OkObjectResult>(actionResultResponse);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(response.Value);

            Assert.NotNull(products);

            var productsList  = products.ToList();
            Assert.Equal(productsList.Count, 2);
            Assert.Equal(productsList[0].Id, 1);
            Assert.Equal(productsList[1].Id, 2);

            _productsRepository.VerifyAll();
        }

        [Fact]
        public async Task can_get_all_products_by_rating()
        {
            _productsRepository.Setup(x => x.GetProductsByRating(It.IsAny<SearchFilter>())).ReturnsAsync(
                new List<Product>
                {
                    new Product { Id = 1 },
                    new Product { Id = 2 }
                });
            
            var actionResultResponse = await _productsController.GetProductsByRating(null);

            var response = Assert.IsType<OkObjectResult>(actionResultResponse);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(response.Value);

            Assert.NotNull(products);

            var productsList  = products.ToList();
            Assert.Equal(productsList.Count, 2);
            Assert.Equal(productsList[0].Id, 1);
            Assert.Equal(productsList[1].Id, 2);

            _productsRepository.VerifyAll();
        }
    }
}