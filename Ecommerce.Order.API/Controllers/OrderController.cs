using Ecommerce.Order.Application.Order;
using Ecommerce.Order.Application.Order.Dto;
using Ecommerce.Order.Application.OrderSession;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOrderService _orderService;
        private readonly IOrderSessionService _orderSessionService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        #region Web API Methods
        [HttpGet("GetOrderById")]

        public async Task<IActionResult> GetOrderById(int OrderId)
        {
            var result = await _orderService.GetOrder(OrderId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] string responseContentType = "application/x-protobuf")
        {
            var result = await _orderService.GetAllOrders();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("SaveOrder")]
        public async Task<IActionResult> SaveOrder(int userId, [FromBody] OrderDto OrderDto)
        {
            var result = await _orderService.SaveOrder(userId, OrderDto);

            if (result == null)
                return NotFound();

            return Created($"/{result.Id}", result);
        }
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DelteOrder(int OrderId)
        {
            var result = await _orderService.DeleteOrder(OrderId);
            if (!result)
                return NotFound();

            return Ok(result);
        }
        #endregion
    }
}
