using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Products.BusinessLogic;
using Products.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        /// <summary>
        /// Retrieves all the products from repository
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore  = true)]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [SwaggerOperation(operationId: "getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productsRepository.GetAllProducts());
        }

        /// <summary>
        /// Retrieves  all the products based on the price filter with min and max value
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("Price")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore  = true)]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [SwaggerOperation(operationId: "getProductsByPrice")]
        public async Task<IActionResult> GetProductsByPrice(SearchFilter filter)
        {
            return Ok(await _productsRepository.GetProductsByPrice(filter));
        }

        /// <summary>
        /// Retrieves all the fantastic products from the repository
        /// </summary>
        /// <returns></returns>
        [HttpGet("Fantastic")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore  = true)]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [SwaggerOperation(operationId: "getFantasticProducts")]
        public async Task<IActionResult> GetFantasticProducts()
        {
            return Ok(await _productsRepository.GetFantasticProducts());
        }

        /// <summary>
        /// Retrieves all the products based on the rating filter with min and max value
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("Rating")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore  = true)]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [SwaggerOperation(operationId: "getProductsByRating")]
        public async Task<IActionResult> GetProductsByRating(SearchFilter filter)
        {
            return Ok(await _productsRepository.GetProductsByRating(filter));
        }
    }
}
