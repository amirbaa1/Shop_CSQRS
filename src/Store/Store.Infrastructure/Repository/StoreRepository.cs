using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Domain.Model;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;
using Store.Infrastructure.Data;
using System.Net;
using Newtonsoft.Json;
using EventBus.Messages.Event.Product;
using MassTransit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Store.Infrastructure.Repository
{
    public class StoreRepository : IStoreRespository
    {
        private readonly StoreDbContext _context;
        private readonly ILogger<StoreRepository> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StoreRepository(StoreDbContext context, ILogger<StoreRepository> logger,
            IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ResultDto> CreateStore(StoreDto storeDto)
        {
            var getProduct = await _context.storeModels.FirstOrDefaultAsync(x => x.ProductId == storeDto.ProductId);
            if (getProduct == null)
            {
                var createStore = new StoreModel
                {
                    Id = Guid.NewGuid(),
                    ProductId = storeDto.ProductId,
                    ProductName = storeDto.ProductName,
                    Number = storeDto.Number,
                    Price = storeDto.Price,
                    Status = storeDto.Number > 0 ? ProductStatus.Available : ProductStatus.OutOfStock,
                    CreateTime = DateTime.UtcNow,
                    UpdateTimeStatus = DateTime.UtcNow,
                };
                if (createStore == null)
                {
                    return new ResultDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccessful = true,
                        Message = "error in create product."
                    };
                }

                _context.storeModels.Add(createStore);
                await _context.SaveChangesAsync();
                return new ResultDto
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccessful = true,
                    Message = "Store created successfully"
                };
            }

            return new ResultDto
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                Message = "Product already exists"
            };
        }

        public async Task<ResultDto> UpdateProductName(UpdateProductNameDto updateName)
        {
            var getStore = await _context.storeModels.SingleOrDefaultAsync(x => x.ProductId == updateName.Id);
            if (getStore == null)
            {
                return new ResultDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    Message = "Not found id store"
                };
            }

            getStore.ProductName = updateName.Name;
            getStore.Price = updateName.Price;
            getStore.UpdateTimeProduct = DateTime.UtcNow;

            _context.storeModels.Update(getStore);

            await _context.SaveChangesAsync();
            return new ResultDto
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                Message = "Change name in store."
            };
        }

        public async Task<ResultDto> UpdateStatusProduct(UpdateStatusProductDto updateStatusProductDto)
        {
            var getProduct =
                await _context.storeModels.FirstOrDefaultAsync(x => x.ProductId == updateStatusProductDto.productId);
            if (getProduct == null)
            {
                return new ResultDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    Message = "Not found product in store"
                };
            }

            if (updateStatusProductDto.Number == 0)
            {
                getProduct.Number = 0;
                getProduct.Status = ProductStatus.OutOfStock;
                getProduct.UpdateTimeStatus = DateTime.UtcNow;

                _context.storeModels.Update(getProduct);
                await _context.SaveChangesAsync();

                var message = new UpdateProductStatusEvent
                {
                    ProductId = getProduct.ProductId,
                    Number = getProduct.Number,
                    ProductStatus = (ProductStatusEvent)getProduct.Status,
                };

                await _context.SaveChangesAsync();
                await _publishEndpoint.Publish(message);

                return new ResultDto
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccessful = true,
                    Message = $"change product status : {JsonConvert.SerializeObject(getProduct)}"
                };
            }
            else
            {
                if (getProduct.Number > 0)
                {
                    getProduct.Number += updateStatusProductDto.Number;
                    getProduct.Status = ProductStatus.Available;
                    getProduct.UpdateTimeStatus = DateTime.UtcNow;


                    _context.storeModels.Update(getProduct);
                    var message = new UpdateProductStatusEvent
                    {
                        ProductId = getProduct.ProductId,
                        Number = getProduct.Number,
                        ProductStatus = (ProductStatusEvent)getProduct.Status,
                    };

                    await _context.SaveChangesAsync();
                    await _publishEndpoint.Publish(message);
                    _logger.LogInformation($"{JsonConvert.SerializeObject(getProduct)}");
                    _logger.LogInformation($"message---->{JsonConvert.SerializeObject(message)}");

                    return new ResultDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccessful = true,
                        Message = $"change product status : {getProduct}"
                    };
                }

                // if number == 0
                getProduct.Number = updateStatusProductDto.Number;
                getProduct.Status = ProductStatus.Available;
                getProduct.UpdateTimeStatus = DateTime.UtcNow;

                _context.storeModels.Update(getProduct);

                var updatedMessage = new UpdateProductStatusEvent
                {
                    ProductId = getProduct.ProductId,
                    Number = getProduct.Number,
                    ProductStatus = (ProductStatusEvent)getProduct.Status,
                };

                await _context.SaveChangesAsync();

                await _publishEndpoint.Publish(updatedMessage);

                _logger.LogInformation($"{JsonConvert.SerializeObject(getProduct)}");

                return new ResultDto
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccessful = true,
                    Message = $"change product status : {JsonConvert.SerializeObject(getProduct)}"
                };
            }
        }

        public async Task<ResultDto> DeleteStore(Guid productId)
        {
            var getProduct = await _context.storeModels.SingleOrDefaultAsync(x => x.ProductId == productId);
            if (getProduct == null)
            {
                return new ResultDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    Message = "Not found product in store"
                };
            }

            _context.storeModels.Remove(getProduct);

            await _context.SaveChangesAsync();

            return new ResultDto
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                Message = "Delete product in store"
            };
        }

        public Task<List<StoreDto>> GetStore()
        {
            var store = _context.storeModels.OrderByDescending(x => x.Id).Select(x => new StoreDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Number = x.Number,
                Price = x.Price,
                Status = x.Status,
                CreateTime = x.CreateTime,
                LastUpdateTimeStatus = x.UpdateTimeStatus,
                LastUpdateTimeProduct = x.UpdateTimeProduct,
            }).ToListAsync();

            return store;
        }

        public async Task<ResultDto> UpdateInventoryAfterPurchase(UpdateNumberDto update)
        {
            var getStore = await _context.storeModels.FirstOrDefaultAsync(x => x.ProductId == update.ProductId);

            if (getStore == null)
            {
                return new ResultDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    Message = "Not found store in database."
                };
            }

            var updateNumber = getStore.Number - update.Number;
            getStore.Number = updateNumber;

            if (updateNumber < 0)
            {
                return new ResultDto
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    Message = $"Only product number :{getStore.Number}"
                };
            }

            if (updateNumber == 0)
            {
                getStore.Status = ProductStatus.OutOfStock;
            }
            else
            {
                getStore.Status = ProductStatus.Available;
            }

            getStore.UpdateTimeStatus = DateTime.UtcNow;

            _logger.LogInformation($"---> update : {getStore}");

            _context.storeModels.Update(getStore);

            var message = new UpdateProductStatusEvent
            {
                ProductId = getStore.ProductId,
                Number = updateNumber,
                ProductStatus = (ProductStatusEvent)getStore.Status,
            };

            await _context.SaveChangesAsync();
            await _publishEndpoint.Publish(message);

            return new ResultDto
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                Message = "Update store"
            };
        }
    }
}