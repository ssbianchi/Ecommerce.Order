using Ecommerce.Order.Application.Order.Dto;
using Ecommerce.Order.Domain.Entity.Readonly.Dapper.Order;

namespace Ecommerce.Order.Application.Order.Profile
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Ecommerce.Order.Domain.Entity.Order.Order, OrderDto>().ReverseMap();
            CreateMap<DapperOrder, OrderDto>().ReverseMap();
        }
    }
}
