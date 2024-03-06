using Ecommerce.Order.Application.Order.Dto;

namespace Ecommerce.Order.Application.Order
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrder(int orderId);
        Task<List<OrderDto>> GetAllOrders();
        Task<OrderDto> SaveOrder(int userId, OrderDto orderDto);
        Task<bool> CloseOrderSession(int orderSessionId, int orderSessionStatusId);
        Task<bool> CloseOrder(int userId);
        Task<bool> DeleteOrder(int orderdId);
    }
}
