using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories.Contract;


using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using AutoMapper;
using Talapat.Api.DTOs.Product;
using Microsoft.EntityFrameworkCore;
using Talapat.Api.Errors;

namespace Talapat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductBrand> _ProductBrandsRepository;
        private readonly IGenericRepository<ProductCategory> _ProductCategorysRepository;
        private readonly IMapper _mapper;

        public ProductsController
            (IGenericRepository<Product> ProductsRepository,
            IMapper mapper,
            IGenericRepository<ProductBrand> ProductBrandsRepository,
            IGenericRepository<ProductCategory> ProductCategorysRepository)

        {
            _ProductBrandsRepository = ProductBrandsRepository;
            _ProductCategorysRepository = ProductCategorysRepository;
            _productsRepository = ProductsRepository;
            _mapper = mapper;
        }

        //  /api/products

        [ProducesResponseType(typeof(ProductToGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToGetDto>>> GetProducts(string? sort,int? brandId,int? categoryId  )
        {
            var spec = new ProductSpecificationWithProductCategory(sort,brandId,categoryId);
            var products = await _productsRepository.GetAllWithSpecAsync(spec);
           //return await _productsRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<Product>, IReadOnlyList<ProductToGetDto>>(products));


        }

        // /api/products/id 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToGetDto>> GetProductById(int id)
        {
            var spec = new ProductSpecificationWithProductCategory(id);

            var product = await _productsRepository.GetWithSpecAsync(spec);
            if (product == null) return NotFound(new { message = "this record couldnot founded", StatusCode = 404 }); //404
            return Ok(_mapper.Map<Product, ProductToGetDto>(product));
        }
        // /api/products/brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            var brands = await _ProductBrandsRepository.GetAllAsync();
            return Ok(brands);

             
        }
        // /api/products/categorys
        [HttpGet("Categorys")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategorys()
        {
            var categorys = await _ProductCategorysRepository.GetAllAsync();
            return Ok(categorys);
        }
    }
}
