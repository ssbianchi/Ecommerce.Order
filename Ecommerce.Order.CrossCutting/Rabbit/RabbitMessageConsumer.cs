using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.CrossCutting.Rabbit
{
    public class RabbitMessageConsumer
    {
        public int OrderSessionId { get; set; }
        public int OrderSessionStatusId { get; set; }
    }
}
