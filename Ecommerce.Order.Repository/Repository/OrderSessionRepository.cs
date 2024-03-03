using Ecommerce.Order.Domain.Entity.OrderSession;
using Ecommerce.Order.Domain.Entity.OrderSession.Repository;
using Ecommerce.Order.Repository.Context;

namespace Ecommerce.Order.Repository.Repository
{
    public class OrderSessionRepository : UnitOfWork<OrderSession>, IOrderSessionRepository
    {
        public OrderSessionRepository(EcommerceContext context) : base(context)
        {
        }
    }
}