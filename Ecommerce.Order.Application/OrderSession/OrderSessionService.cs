using AutoMapper;
using Ecommerce.Order.Application.Order.Dto;
using Ecommerce.Order.Application.Order;
using Ecommerce.Order.Application.OrderSession.Dto;
using Ecommerce.Order.Application.Shared;
using Ecommerce.Order.Domain.Entity.Order.Repository;
using Ecommerce.Order.Domain.Entity.OrderSession.Repository;
using Ecommerce.Order.Domain.Entity.Readonly.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Application.OrderSession
{
    public class OrderSessionService : AbstractService, IOrderSessionService
    {
        private readonly IOrderSessionRepository _orderSessionRepository;

        public OrderSessionService(IOrderSessionRepository orderSessionRepository, IMapper mapper)
            : base(mapper)
        {
            _orderSessionRepository = orderSessionRepository;
        }
        public async Task<OrderSessionDto> GetOrderSession(int orderId)
        {
            var result = await _orderSessionRepository.GetOneByCriteria(a => a.Id == orderId);
            return _mapper.Map<OrderSessionDto>(result);
        }
        public async Task<OrderSessionDto> GetOrderSessionByUserId(int userId)
        {
            var result = await _orderSessionRepository.GetOneByCriteria(a => a.UserId == userId);
            return _mapper.Map<OrderSessionDto>(result);
        }
        public async Task<OrderSessionDto> SaveOrderSession(OrderSessionDto orderSessionDto)
        {
            using (var transaction = await _orderSessionRepository.CreateTransaction())
            {
                try
                {
                    var result = await SaveUpdateDeleteDto(orderSessionDto, _orderSessionRepository);

                    await transaction.CommitAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public async Task<bool> DeleteOrderSession(int orderdSessionId)
        {
            using (var transaction = await _orderSessionRepository.CreateTransaction())
            {
                try
                {
                    var Order = await _orderSessionRepository.GetOneByCriteria(a => a.Id == orderdSessionId);

                    await _orderSessionRepository.Delete(Order);

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
