﻿using AutoMapper;
using Ecommerce.Order.Application.Order.Dto;
using Ecommerce.Order.Application.OrderSession;
using Ecommerce.Order.Application.OrderSession.Dto;
using Ecommerce.Order.Application.Shared;
using Ecommerce.Order.CrossCutting.Enumeration;
using Ecommerce.Order.Domain.Entity.Order.Repository;
using Ecommerce.Order.Domain.Entity.OrderSession.Repository;
using Ecommerce.Order.Domain.Entity.Readonly.Repository;

namespace Ecommerce.Order.Application.Order
{
    //public class OrderService : IOrderService
    public class OrderService : AbstractService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReadonlyRepository _readonlyRepository;
        private readonly IOrderSessionRepository _orderSessionRepository;

        public OrderService(IOrderRepository orderRepository, IOrderSessionRepository orderSessionRepository, IMapper mapper, IReadonlyRepository readonlyRepository)
            : base(mapper)
        {
            _orderRepository = orderRepository;
            _orderSessionRepository = orderSessionRepository;
            //_mapper = mapper;
            _readonlyRepository = readonlyRepository;
        }
        public async Task<OrderDto> GetOrder(int orderId)
        {
            var result = await _orderRepository.GetOneByCriteria(a => a.Id == orderId);
            return _mapper.Map<OrderDto>(result);
        }
        public async Task<List<OrderDto>> GetAllOrders()
        {
            var result = await _readonlyRepository.GetAllOrder();
            return _mapper.Map<List<OrderDto>>(result);
        }

        //public async Task<OrderDto> SaveOrder(OrderDto OrderDto)
        //{
        //    var entity = _mapper.Map<Ecommerce.Order.Domain.Entity.Order.Order>(OrderDto);
        //    try
        //    {
        //        if (OrderDto.Id > 0)
        //            await _OrderRepository.Update(entity);
        //        else
        //            await _OrderRepository.Save(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)
        //            throw ex.InnerException;
        //        throw;
        //    }
        //    return _mapper.Map<OrderDto>(entity);
        //}
        public async Task<OrderDto> SaveOrder(int userId, OrderDto orderDto)
        {
            using (var transaction = await _orderRepository.CreateTransaction())
            {
                try
                {
                    if (orderDto.Qtd <= 0)
                        throw new System.Exception("Favor inserir uma quantidade válida");

                    var hasSession = _mapper.Map<OrderSessionDto>(await _orderSessionRepository.GetOneByCriteria(a => a.UserId == userId));

                    if (hasSession == null)
                    {
                        var session = new OrderSessionDto() { UserId = userId, CreatedAt = DateTime.Now, OperationId = (int)OperationEnum.None };
                        hasSession = await SaveUpdateDeleteDto(session, _orderSessionRepository);
                    }
                    orderDto.SessionId = hasSession.Id;

                    //var hasOrder = await _orderRepository.GetOneByCriteria(a => a.ProductId == orderDto.ProductId && a.SessionId == orderDto.SessionId);
                    //if (hasOrder != null)
                    //{
                    //    orderDto.Id = hasOrder.Id;
                    //    orderDto.OperationId = (int)OperationEnum.HasChanges;
                    //}
                    
                    var result = await SaveUpdateDeleteDto(orderDto, _orderRepository);

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
        public async Task<bool> DeleteOrder(int OrderdId)
        {
            using (var transaction = await _orderRepository.CreateTransaction())
            {
                try
                {
                    var Order = await _orderRepository.GetOneByCriteria(a => a.Id == OrderdId);

                    await _orderRepository.Delete(Order);

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
