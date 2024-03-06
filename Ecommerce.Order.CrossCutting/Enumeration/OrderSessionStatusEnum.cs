using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.CrossCutting.Enumeration
{
    public enum OrderSessionStatusEnum
    {
        NotSet = 0,
        Complete = 1,
        Problem = 2,
    }
}
