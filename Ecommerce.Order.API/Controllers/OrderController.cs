using Ecommerce.Order.Application.Order;
using Ecommerce.Order.Application.Order.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOrderService _OrderService;

        public OrderController(ILogger<OrderController> logger, IOrderService OrderService)
        {
            _logger = logger;
            _OrderService = OrderService;
        }
        #region Web API Methods
        [HttpGet("GetOrderById")]

        public async Task<IActionResult> GetOrderById(int OrderId)
        {
            var result = await _OrderService.GetOrder(OrderId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] string responseContentType = "application/x-protobuf")
        {
            var result = await _OrderService.GetAllOrders();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("SaveOrder")]
        public async Task<IActionResult> SaveOrder([FromBody] OrderDto OrderDto)
        {
            var result = await _OrderService.SaveOrder(OrderDto);

            if (result == null)
                return NotFound();

            return Created($"/{result.Id}", result);
        }
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DelteOrder(int OrderId)
        {
            var result = await _OrderService.DeleteOrder(OrderId);
            if (!result)
                return NotFound();

            return Ok(result);
        }
        #endregion
    }
}
