using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Products.Models;
using Xunit;

namespace Products.IntegrationTests
{
    public class ProductsControllerTests : TestBase, IDisposable
    {
        [Fact]
        public async Task can_get_all_products()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);

                var productsList = products.ToList();

                Assert.True(productsList.Count == 3);
                Assert.Equal(productsList[0].Id, 1);
                Assert.Equal(productsList[1].Id, 2);
                Assert.Equal(productsList[2].Id, 3);
            }
        }

        [Fact]
        public async Task can_get_all_products_with_minimum_price_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/price?min=400");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);

                var productsList = products.ToList();

                Assert.True(productsList.Count == 2);
                Assert.Equal(productsList[0].Id, 2);
                Assert.Equal(productsList[1].Id, 3);
            }
        }

        [Fact]
        public async Task can_get_all_products_with_maximum_price_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/price?max=400");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);

                var productsList = products.ToList();

                Assert.True(productsList.Count == 2);
                Assert.Equal(productsList[0].Id, 1);
                Assert.Equal(productsList[1].Id, 2);
            }
        }

        [Fact]
        public async Task can_get_all_products_with_minimum_and_maximum_price_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/price?min=450&max=500");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);

                var productsList = products.ToList();
                
                Assert.True(productsList.Count == 1);
                Assert.Equal(productsList[0].Id, 3);
            }
        }

        [Fact]
        public async Task returns_no_products_with_no_minimum_and_no_maximum_price_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/price");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);

                var productsList = products.ToList();

                Assert.True(productsList.Count == 0);
            }
        }

        [Fact]
        public async Task can_get_all_fantastic_products()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/fantastic");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);

                var productsList = products.ToList();
            
                Assert.True(productsList.Count == 2);
                Assert.Equal(productsList[0].Id, 1);
                Assert.Equal(productsList[1].Id, 3);
            }
        }

        [Fact]
        public async Task can_get_all_products_with_minimum_rating_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/rating?min=4");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);
                var productsList = products.ToList();
            
                Assert.True(productsList.Count == 1);
                Assert.Equal(productsList[0].Id, 1);
            }
        }

        [Fact]
        public async Task can_get_all_products_with_maximum_rating_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/rating?max=3");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);
                var productsList = products.ToList();
            
                Assert.True(productsList.Count == 1);
                Assert.Equal(productsList[0].Id, 2);
            }
        }

                [Fact]
        public async Task can_get_all_products_with_minimum_and_maximum_rating_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/rating?min=3&max=4");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);

                var productsList = products.ToList();
            
                Assert.True(productsList.Count == 1);
                Assert.Equal(productsList[0].Id, 3);
            }
        }

        [Fact]
        public async Task returns_no_products_with_no_minimum_and_no_maximum_rating_range()
        {
            using(var client = _server.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/api/products/rating");

                Assert.Equal(response.StatusCode, HttpStatusCode.OK);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(products);
                var productsList = products.ToList();
                
                Assert.True(productsList.Count == 0);
            }
        }

        public void Dispose()
        {
            if(_server != null)
            {
                _server.Dispose();
            }
        }
    }
}