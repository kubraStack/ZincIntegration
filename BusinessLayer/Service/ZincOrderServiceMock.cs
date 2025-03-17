using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }


            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
