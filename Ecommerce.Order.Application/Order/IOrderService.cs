using Ecommerce.Order.Application.Order.Dto;

namespace Ecommerce.Order.Application.Order
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrder(int OrderId);
        Task<List<OrderDto>> GetAllOrders();
        Task<OrderDto> SaveOrder(OrderDto OrderDto);
        Task<bool> DeleteOrder(int OrderdId);
    }
}
