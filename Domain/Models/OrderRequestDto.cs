using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class OrderRequestDto
    {
        public string ProductId { get; set; }
        public string Platform { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
    }
}
