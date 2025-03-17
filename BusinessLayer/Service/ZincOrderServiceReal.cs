using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class ZincOrderServiceReal : IZincOrderService
    {
        private readonly ZincDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ZincOrderServiceReal(ZincDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _apiKey = configuration["ZincSettings:ApiKey"];
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetAllOrdersAsync() => await _context.Orders.ToListAsync();



        public async Task<Order?> GetOrderByIdAsync(int id) => await _context.Orders.FindAsync(id);


        public async Task<OrderResponseDto> PlaceOrderAsync(OrderRequestDto orderRequestDto)
        {
            //Create JSON Body for Zinc API key
            var zincOrderRequest = new
            {
                client_id = "test-client-id",
                client_secret = _apiKey,
                order = new
                {
                    product_id = orderRequestDto.ProductId,
                    platform = orderRequestDto.Platform,
                    shipping_address = orderRequestDto.ShippingAddress,
                    customer_name = orderRequestDto.CustomerName
                }
            };

            var jsonBody = JsonSerializer.Serialize(zincOrderRequest);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.zinc.io/v1/orders", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Zinc API request failed: " + response.ReasonPhrase);
            }

            var responseContent = await response.Content.ReadAsStringAsync();


            //Parse Zinc API response
            var zincResponse = JsonSerializer.Deserialize<ZincOrderApiResponse>(responseContent);

            var order = new Order
            {
                ProductId = orderRequestDto.ProductId,
                Platform = orderRequestDto.Platform,
                CustomerName = orderRequestDto.CustomerName,
                ShippingAddress = orderRequestDto.ShippingAddress,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"
            };

            await _context.Orders.AddAsync(order);

            return new OrderResponseDto
            {
                OrderId = order.Id,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TrackingNumber = zincResponse.tracking_number,
                EstimatedDeliveryDate = zincResponse.estimated_delivery_date

            };
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public class ZincOrderApiResponse
        {
            public string tracking_number { get; set; }
            public string estimated_delivery_date { get; set; }
        }
    }
}
