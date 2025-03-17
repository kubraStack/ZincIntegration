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
    }
}
