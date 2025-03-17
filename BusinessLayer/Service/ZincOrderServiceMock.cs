using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class ZincOrderServiceMock : IZincOrderService
    {
        private readonly ZincDbContext _context;

        public ZincOrderServiceMock(ZincDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(OrderRequestDto orderRequestDto)
        {
            var order = new Order
            {
                ProductId = orderRequestDto.ProductId,
                Platform = orderRequestDto.Platform,
                CustomerName = orderRequestDto.CustomerName,
                ShippingAddress = orderRequestDto.ShippingAddress,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"

            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderResponseDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                Status = "Confirmed",
                TrackingNumber = $"TRK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(5).ToString("yyyy-MM-dd")
            };
        }
    }
}
