using Microsoft.AspNetCore.Mvc;
using VideoApi.Data.Entities;
using VideoApi.Data.Repositories;
using VideoApi.Services;
using VideoApi.Controllers.DTOs;

namespace VideoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService _cacheService;

        public ProductsController(IProductRepository productRepository, ICacheService cacheService)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }
          [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is required.");

            // Map the DTO to a Product entity
            var product = new Product
            {
                Name = productDto.Name
            };

            // Add the new product to the database
            bool result = await _productRepository.AddProductAsync(product);

            if (!result)
                return StatusCode(500, "An error occurred while creating the product.");

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var cachedVideoFiles = await _cacheService.GetCachedVideoFilesAsync(id);
            product.VideoFiles = cachedVideoFiles.ToList();

            return Ok(product);
        }

        // Add other methods for handling product-related operations
    }
}