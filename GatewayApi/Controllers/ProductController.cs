using GatewayApi.BLL;
using GatewayApi.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApiFpts.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.WebSockets;

namespace ProjectApiFpts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                CDataServiceClient ds = new CDataServiceClient(_configuration);
                products = ds.api_get_all_product();

                return Ok(products);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetProductByIdAsync(string productId)
        {
            ProductModel product = new ProductModel();

            try
            {
                CDataServiceClient ds = new CDataServiceClient(_configuration);
                product = ds.api_get_product_by_id(productId);

                return Ok(product);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProduct(ProductModel product)
        {
            ProductModel newProduct = new ProductModel();

            try
            {
                CDataServiceClient ds = new CDataServiceClient(_configuration);
                newProduct = ds.api_add_product(product);

                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
