using EntityLayer.Entities;

namespace BusinessLayer.Service
{
    public class ZincOrderServiceMockBase
    {

        public Task<OrderResponseDto> PlaceOrderAsync(OrderRequestDto orderRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}