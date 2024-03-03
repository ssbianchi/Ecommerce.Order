using Ecommerce.Order.CrossCutting.Entity;

namespace Ecommerce.Order.Application.Order.Dto
{
    public class OrderDto : OperationEntity<int>
    {
        public int SessionId { get; set; }
        public int ProductId { get; set; }
        public int Qtd { get; set; }
    }
}
