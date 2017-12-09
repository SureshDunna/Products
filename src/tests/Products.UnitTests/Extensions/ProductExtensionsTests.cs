using System;
using Products.BusinessLogic;
using Products.Extensions;
using Xunit;

namespace Products.UnitTests.Extensions
{
    public class ProductExtensionsTests
    {
        [Fact]
        public void must_throw_exception_if_product_is_null()
        {
            BsonProduct product = null;

            Assert.Throws<ArgumentException>(() => product.ToExternalProduct());
        }

        [Fact]
        public void can_get_product_without_attribute()
        {
            var bsonProduct = new BsonProduct
            {
                Id = 1,
                Sku = "sku",
                Name = "product1",
                Price = 5
            };

            var externalProduct = bsonProduct.ToExternalProduct();

            Assert.NotNull(externalProduct);
            Assert.Equal(externalProduct.Id, 1);
            Assert.Equal(externalProduct.Sku, "sku");
            Assert.Equal(externalProduct.Name, "product1");
            Assert.Equal(externalProduct.Price, 5);
            Assert.Null(externalProduct.Attribute);
        }

        [Fact]
        public void can_get_product_attribute()
        {
            var bsonProduct = new BsonProduct
            {
                Attribute = new BsonAttribute
                {
                    Fantastic = new BsonFantastic
                    {
                        Value = true,
                        Type = 1,
                        Name = "fantastic"
                    },
                    Rating = new  BsonRating
                    {
                        Value = 1,
                        Type = "rating",
                        Name = "rating"
                    }
                }
            };

            var externalProduct = bsonProduct.ToExternalProduct();

            Assert.NotNull(externalProduct);
            Assert.NotNull(externalProduct.Attribute);
            Assert.NotNull(externalProduct.Attribute.Fantastic);
            Assert.NotNull(externalProduct.Attribute.Rating);
            Assert.Equal(externalProduct.Attribute.Fantastic.Name, "fantastic");
            Assert.Equal(externalProduct.Attribute.Fantastic.Value, true);
            Assert.Equal(externalProduct.Attribute.Fantastic.Type, 1);
            Assert.Equal(externalProduct.Attribute.Rating.Name, "rating");
            Assert.Equal(externalProduct.Attribute.Rating.Value, 1);
            Assert.Equal(externalProduct.Attribute.Rating.Type, "rating");
        }
    }
}