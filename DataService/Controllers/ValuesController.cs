using DataService.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApiFpts.Models;

namespace DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ValuesController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            try
            {          
                
                return Ok(await _productRepository.GetAllProductAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = _productRepository.GetProductByIdAsync(productId);

                if (product != null)
                {
                    return Ok(await product);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(ProductModel product)
        {
            try
            {
                var newProduct = _productRepository.AddProductAsync(product);
                if (newProduct != null)
                {
                    return Ok(await newProduct);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
