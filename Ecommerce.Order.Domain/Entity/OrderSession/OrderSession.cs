using Ecommerce.Order.CrossCutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Domain.Entity.OrderSession
{
    public class OrderSession : Entity<int>
    {
        public int UserId { get; set; }
        public int OrderSessionStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
