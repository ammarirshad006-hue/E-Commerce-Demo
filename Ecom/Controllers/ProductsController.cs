using Ecom.Models;
using Ecom.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all products");
                var products = await _productService.GetAllProductsAsync();

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products found");
                    return NotFound("No products available.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching product with Id: {Id}", id);

                var product = await _productService.GetProductByIdAsync(id);


                if (product == null)
                {
                    _logger.LogWarning("Product with Id {Id} not found", id);
                    return NotFound($"Product with Id {id} not found.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/products

        //        public async Task<IActionResult> Add([FromBody] Product product)
        //        {
        //            try
        //            {
        //                _logger.LogInformation("Creating a new product: {@Product}", product)
        //;
        //                if (product == null)
        //                {
        //                    _logger.LogWarning("Product data is null, creation aborted");
        //                    return BadRequest("Product data cannot be null.");
        //                }

        //                var createdProduct = await _productService.AddProductAsync(product);

        //                _logger.LogInformation("Product created successfully: {@Product}", createdProduct);

        //                return CreatedAtAction(nameof(GetAll), new { id = createdProduct.Id }, createdProduct);
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError(ex, "Error occurred while creating product");
        //                return StatusCode(500, "Internal server error");
        //            }
        //        }



        // POST: api/products
        [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product data cannot be null.");

            // Ensure no ID is sent by client
            product.Id = 0;
            product.Stock = null; // prevent EF trying to insert Stocks at creation

            try
            {
                var createdProduct = await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, "Error creating product: {Message}", innerMessage);
                return StatusCode(500, new { Message = innerMessage });
            }
        }


        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            try
            {
                _logger.LogInformation("Updating product with ID {ProductId}: {@Product}", id, product);

                if (product == null || product.Id != id)
                {
                    _logger.LogWarning("Product data is null or ID mismatch");
                    return BadRequest("Invalid product data.");
                }

                var updatedProduct = await _productService.UpdateProductAsync(product);

                if (updatedProduct == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", id);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("Product updated successfully with ID {ProductId}", updatedProduct.Id);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product with ID {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("lowstock/{threshold}")]
        public async Task<IActionResult> GetLowStock(int threshold)
        {
            var lowStockProducts = await _productService.GetLowStockProductsAsync(threshold);
            return Ok(lowStockProducts);
        }


    
}
