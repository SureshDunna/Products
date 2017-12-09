using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Products.DataAccess;
using Products.Extensions;
using Products.Models;

namespace Products.BusinessLogic
{
    /// <summary>
    /// Products repository where CRUD operations can be applied for
    /// </summary>
    public class ProductsRepository : BaseMongoRepository, IProductsRepository
    {
        private const string CollectionName = "Products";

        public ProductsRepository(IMongoDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            return Task.FromResult(GetQueryable<BsonProduct>(CollectionName).AsEnumerable().Select(p => p.ToExternalProduct()));
        }

        public Task<IEnumerable<Product>> GetProductsByPrice(SearchFilter searchFilter)
        {
            if(searchFilter.Min == 0)
            {
                var productsWithMax = from p in GetQueryable<BsonProduct>(CollectionName)
                where p.Price <= searchFilter.Max
                select p;

                return Task.FromResult(productsWithMax.AsEnumerable().Select(p => p.ToExternalProduct()));
            }

            if(searchFilter.Max == 0)
            {
                var productsWithMin = from p in GetQueryable<BsonProduct>(CollectionName)
                where p.Price >= searchFilter.Min
                select p;

                return Task.FromResult(productsWithMin.AsEnumerable().Select(p => p.ToExternalProduct()));
            }
            
            var products = from p in GetQueryable<BsonProduct>(CollectionName)
            where p.Price >= searchFilter.Min && p.Price <= searchFilter.Max
            select p;

            return Task.FromResult(products.AsEnumerable().Select(p => p.ToExternalProduct()));
        }

        public Task<IEnumerable<Product>> GetFantasticProducts()
        {
            var products =from p in GetQueryable<BsonProduct>(CollectionName)
            where p.Attribute != null && p.Attribute.Fantastic != null && p.Attribute.Fantastic.Value
            select p;

            return Task.FromResult(products.AsEnumerable().Select(p => p.ToExternalProduct()));
        }

        public Task<IEnumerable<Product>> GetProductsByRating(SearchFilter filter)
        {
            if(filter.Min == 0)
            {
                var productsWithMax =from p in GetQueryable<BsonProduct>(CollectionName)
                where p.Attribute != null && 
                p.Attribute.Rating != null &&
                p.Attribute.Rating.Value <= filter.Max
                select p;

                return Task.FromResult(productsWithMax.AsEnumerable().Select(p => p.ToExternalProduct()));
            }

            if(filter.Max == 0)
            {
                var productsWithMin =from p in GetQueryable<BsonProduct>(CollectionName)
                where p.Attribute != null && 
                p.Attribute.Rating != null &&
                p.Attribute.Rating.Value >= filter.Min
                select p;

                return Task.FromResult(productsWithMin.AsEnumerable().Select(p => p.ToExternalProduct()));
            }

            var products =from p in GetQueryable<BsonProduct>(CollectionName)
            where p.Attribute != null && 
            p.Attribute.Rating != null &&
            p.Attribute.Rating.Value >= filter.Min && 
            p.Attribute.Rating.Value <= filter.Max
            select p;

            return Task.FromResult(products.AsEnumerable().Select(p => p.ToExternalProduct()));
        }
    }
}