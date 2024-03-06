using Ecommerce.Order.CrossCutting.Entity;

namespace Ecommerce.Order.Application.OrderSession.Dto
{
    public class OrderSessionDto : OperationEntity<int>
    {
        public int UserId { get; set; }
        public int OrderSessionStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
