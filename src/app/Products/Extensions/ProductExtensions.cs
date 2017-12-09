using System;
using Products.BusinessLogic;
using Products.Models;

namespace Products.Extensions
{
    public static class ProductExtensions
    {
        public static Product ToExternalProduct(this BsonProduct product)
        {
            if(product == null)
            {
                throw new ArgumentException(nameof(product));
            }

            var externalProduct = new Product
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Price = product.Price
            };

            if(product.Attribute != null)
            {
                externalProduct.Attribute = new Models.Attribute();

                if(product.Attribute.Fantastic != null)
                {
                    externalProduct.Attribute.Fantastic = new Fantastic
                    {
                        Value = product.Attribute.Fantastic.Value,
                        Type = product.Attribute.Fantastic.Type,
                        Name = product.Attribute.Fantastic.Name,
                    };
                }

                if(product.Attribute.Rating != null)
                {
                    externalProduct.Attribute.Rating = new Rating
                    {
                        Value = product.Attribute.Rating.Value,
                        Type = product.Attribute.Rating.Type,
                        Name = product.Attribute.Rating.Name,
                    };
                }
            }

            return externalProduct;
        }
    }
}