using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Order
    {
        public int Id { get; set; } //Primary key
        public string ProductId { get; set; }
        public string Platform { get; set; } //amazon, ebay
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } //pending, completed, failed
    }
}
