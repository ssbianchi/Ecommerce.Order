using Ecommerce.Order.CrossCutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Application.OrderSession.Dto
{
    public class OrderSessionDto : OperationEntity<int>
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
