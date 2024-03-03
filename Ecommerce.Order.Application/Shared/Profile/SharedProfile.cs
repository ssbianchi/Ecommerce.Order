using Ecommerce.Order.Application.Shared.Dto;
using Ecommerce.Order.Domain.Entity.Readonly.Dapper;

namespace Ecommerce.Order.Application.Shared.Profile
{
    public class SharedProfile : AutoMapper.Profile
    {
        public SharedProfile()
        {
            CreateMap<DapperIdName, IdNameDto>();
        }
    }
}
