using Ecommerce.Order.Application.Order.Dto;
using Ecommerce.Order.Application.OrderSession.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
