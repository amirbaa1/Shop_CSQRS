using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Basket
{
    public class CheckOutStoreRequest
    {
        public Guid ProductId { get; set; }
        public int Number { get; set; }
    }
}
