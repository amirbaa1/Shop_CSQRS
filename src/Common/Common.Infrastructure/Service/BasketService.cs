using Contracts.Basket;
using Contracts.General;
using MassTransit;
using System.Net;


namespace Common.Infrastructure.Service
{
    public class BasketService
    {
        private readonly IRequestClient<CheckOutStoreRequest> _checkStore;
        private readonly IRequestClient<SendToOrderRequest> _sendToOrder;

        public BasketService(IRequestClient<CheckOutStoreRequest> checkStore,
            IRequestClient<SendToOrderRequest> sendToOrder)
        {
            _checkStore = checkStore;
            _sendToOrder = sendToOrder;
        }

        public async Task<ResponseResult> CheckStore(Guid productId, int number)
        {
            var response = await _checkStore.GetResponse<ResponseResult>(new CheckOutStoreRequest
            {
                ProductId = productId,
                Number = number
            });

            if (response.Message.IsSuccessful == false)
            {
                return new ResponseResult
                {
                    IsSuccessful = false,
                    Message = $"message : {response.Message.Message}",
                    StatusCode = HttpStatusCode.BadGateway
                };
            }

            return new ResponseResult
            {
                IsSuccessful = true,
                Message = $"message {response.Message.Message}",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseResult> SendOrder(SendToOrderRequest sendToOrderRequest)
        {
            var response = await _sendToOrder.GetResponse<ResponseResult>(sendToOrderRequest);

            if (response.Message.IsSuccessful == false)
            {
                return new ResponseResult
                {
                    IsSuccessful = false,
                    Message = $"message : {response.Message.Message}",
                    StatusCode = HttpStatusCode.BadGateway
                };
            }

            return new ResponseResult
            {
                IsSuccessful = true,
                Message = $"message {response.Message.Message}",
                StatusCode = HttpStatusCode.OK
            };
        }


        public async Task<ResponseResult> UpdateInventoryStore(Guid productId, int number)
        {
            var response = await _checkStore.GetResponse<ResponseResult>(new SendToOrderRequest
            {
                BasketItems = new List<BasketItemRequest>
                {
                    new BasketItemRequest
                    {
                        ProductId = productId,
                        Quantity = number
                    }
                }
            });
            if (response.Message.IsSuccessful == false)
            {
                return new ResponseResult
                {
                    IsSuccessful = false,
                    Message = $"message : {response.Message.Message}",
                    StatusCode = HttpStatusCode.BadGateway
                };
            }

            return new ResponseResult
            {
                IsSuccessful = true,
                Message = $"message {response.Message.Message}",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}