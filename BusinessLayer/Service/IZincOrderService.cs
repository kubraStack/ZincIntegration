using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IZincOrderService
    {
        Task<OrderResponseDto> PlaceOrderAsync(OrderRequestDto orderRequestDto);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int id, string status);
    }
}
