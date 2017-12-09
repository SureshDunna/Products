using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Models;

namespace Products.BusinessLogic
{
    /// <summary>
    /// Products repository where CRUD operations can be applied for
    /// </summary>
    public interface IProductsRepository
    {
         Task<IEnumerable<Product>> GetAllProducts();
         Task<IEnumerable<Product>> GetProductsByPrice(SearchFilter filter);
         Task<IEnumerable<Product>> GetFantasticProducts();
         Task<IEnumerable<Product>> GetProductsByRating(SearchFilter filter);
    }
}