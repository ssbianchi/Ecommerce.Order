using Ecommerce.Order.Application.OrderSession.Dto;

namespace Ecommerce.Order.Application.OrderSession
{
    public interface IOrderSessionService
    {
        Task<OrderSessionDto> GetOrderSession(int orderSessionId);
        Task<OrderSessionDto> GetOrderSessionByUserId(int userId);
        Task<OrderSessionDto> SaveOrderSession(OrderSessionDto orderSessionDto);
        Task<bool> DeleteOrderSession(int orderSessionId);
    }
}
