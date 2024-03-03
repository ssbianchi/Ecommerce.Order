using Ecommerce.Order.Domain.Entity.Order.Repository;
using Ecommerce.Order.Repository.Context;

namespace Ecommerce.Order.Repository.Repository
{
    public class OrderRepository : UnitOfWork<Ecommerce.Order.Domain.Entity.Order.Order>, IOrderRepository
    {
        public OrderRepository(EcommerceContext context): base(context) 
        { 
        }
    }
}
