using Ecommerce.Order.CrossCutting.Entity;

namespace Ecommerce.Order.Domain.Entity.Order
{
    public class Order : Entity<int>
    {
        public int SessionId { get; set; }
        public int ProductId { get; set; }
        public int Qtd { get; set; }
    }
}
