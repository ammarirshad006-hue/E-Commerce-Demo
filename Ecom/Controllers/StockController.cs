using Ecom.Models;
using Ecom.Repository.Interface;
using Ecom.Service.Implementation;
using Ecom.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepositery _StockRepository;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockRepositery StockRepository, ILogger<StockController> logger)
        {
            _StockRepository = StockRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try

            {
                var stocks = await _StockRepository.GetAllAsync();

                if (stocks == null || !stocks.Any())
                {
                    _logger.LogInformation("No stock records found.");
                    return NotFound("No stock records found.");
                }

                return Ok(stocks);
            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred while fetching stocks.");
                return StatusCode(500, "Internal server error.");

            }


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var stock = await _StockRepository.GetById(id);

                if (stock == null)
                {
                    _logger.LogWarning("Stock with Id {Id} not found.", id);
                    return NotFound($"Stock with Id {id} not found.");
                }

                return Ok(stock);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching stock with Id {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] Stock stock)
        {
            _logger.LogInformation("AddStock called with ProductId = {ProductId}, Quantity = {Qty}", stock.ProductId, stock.Quantity);

            try
            {
                var createdStock = await _StockRepository.AddAsync(stock);

                _logger.LogInformation("Stock created successfully with Id = {Id}", createdStock.Id);

                return Ok(new
                {
                    ProductName = createdStock.ProductId,
                    AvailableQty = createdStock.Quantity,
                    Status = "Stock Added Successfully ✅"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding stock");
                return StatusCode(500, new
                {
                    Message = "Something went wrong while adding stock",
                    Error = ex.Message
                });
            }
        }

        
        
        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] Stock stock)
        {
            
                try
                {
                    if (stock == null || id != stock.Id)
                    {
                        return BadRequest("Invalid stock data.");
                    }

                    var updatedStock = await _StockRepository.UpdateAsync(stock);

                    if (updatedStock == null)
                    {
                        _logger.LogWarning("Stock with Id {Id} not found for update.", id);
                        return NotFound($"Stock with Id {id} not found.");
                    }

                    return Ok(updatedStock);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating stock with Id {Id}", id);
                    return StatusCode(500, "Internal server error.");
                }
            }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedStock = await _StockRepository.DeleteAsync(id);

                if (deletedStock == null)
                {
                    _logger.LogWarning("Stock with Id {Id} not found for deletion.", id);
                    return NotFound($"Stock with Id {id} not found.");
                }

                return Ok(deletedStock); // or NoContent() if you don’t want to return the deleted object
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting stock with Id {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] int productId)
        {
            return Ok($"You searched for product id = {productId}");
        }

    }

}

