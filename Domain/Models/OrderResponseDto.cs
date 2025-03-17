using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class OrderResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? OrderId { get; set; }
    }
}
