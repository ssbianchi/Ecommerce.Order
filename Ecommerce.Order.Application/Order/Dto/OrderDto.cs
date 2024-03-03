using Ecommerce.Order.Application.OrderSession.Dto;
using Ecommerce.Order.CrossCutting.Entity;

namespace Ecommerce.Order.Application.Order.Dto
{
    public class OrderDto : OperationEntity<int>
    {
        //public int UserId { get; set; }
        public int SessionId { get; set; }
        public int ProductId { get; set; }
        public int Qtd { get; set; }
        //public OrderSessionDto Session { get; set; }
    }
}
