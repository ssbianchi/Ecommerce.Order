using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Application.Order.Dto
{
    public class OrderCloseDto
    {
        public int OrderSessionId { get; set; }
        public double Amount { get; set; }
    }
}
