using AutoMapper;
using Ecommerce.Order.Application.Order.Dto;
using Ecommerce.Order.Application.Shared;
using Ecommerce.Order.Domain.Entity.Order.Repository;
using Ecommerce.Order.Domain.Entity.Readonly.Repository;

namespace Ecommerce.Order.Application.Order
{
    //public class OrderService : IOrderService
    public class OrderService : AbstractService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReadonlyRepository _readonlyRepository;

        public OrderService(IOrderRepository OrderRepository, IMapper mapper, IReadonlyRepository readonlyRepository)
            : base(mapper)
        {
            _orderRepository = OrderRepository;
            //_mapper = mapper;
            _readonlyRepository = readonlyRepository;
        }
        public async Task<OrderDto> GetOrder(int OrderId)
        {
            var result = await _orderRepository.GetOneByCriteria(a => a.Id == OrderId);
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
        public async Task<OrderDto> SaveOrder(OrderDto OrderDto)
        {
            using (var transaction = await _orderRepository.CreateTransaction())
            {
                try
                {
                    //if (!OrderDto.Created.HasValue)
                    //    Created.Created = DateTime.Now;

                    var result = await SaveUpdateDeleteDto(OrderDto, _orderRepository);

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
