using Ecommerce.Order.Application.Order;
using Ecommerce.Order.Application.Order.Dto;
using Ecommerce.Order.Application.OrderSession;
using Ecommerce.Order.Application.RabbitRequest;
using Ecommerce.Order.Application.RabbitRequest;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOrderService _orderService;
        private readonly IRabbitRequestService _rabbitRequestService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IRabbitRequestService rabbitRequestService)
        {
            _logger = logger;
            _orderService = orderService;
            _rabbitRequestService = rabbitRequestService;
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
        public async Task<IActionResult> GetAllOrders()
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

            //_rabbitMessageService.SendMessage(result);
            return Created($"/{result.Id}", result);
        }

        [HttpPost("CloseOrder")]
        public async Task<IActionResult> CloseOrder(int userId)
        {
            var result = await _orderService.CloseOrder(userId);

            if (result == null)
                return NotFound();

            //_rabbitRequestService.SendMessage(result);

            return Created($"/", result);
        }
        [HttpPost("CloseOrderSession")]
        public async Task<IActionResult> CloseOrderSession(int orderSessionId, int orderSessionStatusId)
        {
            var result = await _orderService.CloseOrderSession(orderSessionId, orderSessionStatusId);

            if (result == null)
                return NotFound();

            //_rabbitRequestService.SendMessage(result);

            return Created($"/", result);
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
