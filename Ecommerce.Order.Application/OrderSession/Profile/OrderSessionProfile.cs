using Ecommerce.Order.Application.OrderSession.Dto;

namespace Ecommerce.Order.Application.OrderSession.Profile
{
    public class OrderSessionProfile : AutoMapper.Profile
    {
        public OrderSessionProfile()
        {
            CreateMap<Ecommerce.Order.Domain.Entity.OrderSession.OrderSession, OrderSessionDto>().ReverseMap();
        }
    }
}