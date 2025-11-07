using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories.Contract;


using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talapat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepository;

        public ProductsController(IGenericRepository<Product> ProductsRepository)
        {
            _productsRepository = ProductsRepository;
        }

        //  /api/products

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductSpecificationWithProductCategory();
            var products = await _productsRepository.GetAllWithSpecAsync(spec);
            // return await _productsRepository.GetAllAsync();
            return Ok(products);


        }

        // /api/products/id 
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductSpecificationWithProductCategory(id);

            var product = await _productsRepository.GetWithSpecAsync(spec);
            if (product == null) return NotFound(new { message = "this record couldnot founded", StatusCode = 404 }); //404
            return Ok(product);
        }
    }
}
