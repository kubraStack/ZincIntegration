using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string EstimatedDeliveryDate { get; set; }
    }
}
