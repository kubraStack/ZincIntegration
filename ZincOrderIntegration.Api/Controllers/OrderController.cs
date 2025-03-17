using BusinessLayer.Service;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZincOrderIntegration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IZincOrderService _zincOrderService;

        public OrderController(IZincOrderService zincOrderService)
        {
            _zincOrderService = zincOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequestDto requestDto)
        {
            var response = await _zincOrderService.PlaceOrderAsync(requestDto);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _zincOrderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _zincOrderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound("Order not found");
            }
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var isDeleted = await _zincOrderService.DeleteOrderAsync(id);
            if (!isDeleted)
            {
                return NotFound("Order not found");
            }
            return Ok("Order deleted");
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var isUpdated = await _zincOrderService.UpdateOrderStatusAsync(id, status);
            if (!isUpdated)
            {
                return NotFound("Order not found");
            }
            return Ok("Order status updated");
        }

    }
}
