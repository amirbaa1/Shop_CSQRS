using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Domain.Model;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;
using Store.Infrastructure.Data;
using System.Net;


namespace Store.Infrastructure.Repository
{
    public class StoreRepository : IStoreRespository
    {
        private readonly StoreDbContext _context;
        private readonly ILogger<StoreRepository> _logger;

        public StoreRepository(StoreDbContext context, ILogger<StoreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResultDto> CreateStore(StoreDto storeDto)
        {
            var getProduct = await _context.storeModels.SingleOrDefaultAsync(x => x.ProductId == storeDto.ProductId);
            if (getProduct == null)
            {
                var createStore = new StoreModel
                {
                    Id = Guid.NewGuid(),
                    ProductId = storeDto.ProductId,
                    ProductName = storeDto.ProductName,
                    Number = storeDto.Number,
                    Status = storeDto.Number > 0 ? ProductStatus.Available : ProductStatus.OutOfStock,
                    CreateTime = DateTime.UtcNow,
                };
                if (createStore == null)
                {
                    return new ResultDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccessful = true,
                        Message = "problem in create product."
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
                Status = x.Status,
                CreateTime = x.CreateTime,
                LastUpdateTime = x.UpdateTime,
            }).ToListAsync();

            return store;
        }

        public async Task<ResultDto> UpdateStore(UpdateNumberDto update)
        {
            var getStore = await _context.storeModels.FirstOrDefaultAsync(x => x.Id == update.Id);

            if (getStore == null)
            {
                return new ResultDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    Message = "Not found store in database."
                };
            }
            if (update.Number == 0)
            {
                getStore.Status = ProductStatus.OutOfStock;
            }
            else
            {
                getStore.Status = ProductStatus.Available;
            }
            
            
            getStore.Number = update.Number;
            getStore.UpdateTime = DateTime.UtcNow;

            _logger.LogInformation($"---> update : {getStore}");

            _context.storeModels.Update(getStore);
            await _context.SaveChangesAsync();

            return new ResultDto
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                Message = "Update store"
            };
        }
    }
}
