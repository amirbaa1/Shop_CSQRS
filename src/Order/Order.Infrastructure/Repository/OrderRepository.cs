using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order.Domain.Model;
using Order.Domain.Model.Dto;
using Order.Domain.Repository;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderdbContext _orderdbContext;
    private readonly ILogger<OrderRepository> _logger;
    private readonly IProductRepository _productRepository;
    
    public OrderRepository(OrderdbContext orderdbContext, ILogger<OrderRepository> logger,
        IProductRepository productRepository)
    {
        _orderdbContext = orderdbContext;
        _logger = logger;
        _productRepository = productRepository;
    }
    public async Task<List<OrderModelDto>> GetOrdersByUserId(string userid)
    {
        var order = await _orderdbContext.Orders
            .Include(x => x.OrderLines)
            .Where(x => x.UserId == userid)
            .Select(x =>
                new OrderModelDto
                {
                    Id = x.Id,
                    OrderPaid = x.OrderPaid,
                    UserId = x.UserId,
                    TotalPrice = x.TotalPrice,
                    OrderPlaced = x.OrderPlaced,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    ZipCode = x.ZipCode,
                    EmailAddress = x.EmailAddress,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    OrderLines = x.OrderLines.Select(xl => new OrderLineDto
                    {
                        Id = xl.Id,
                        Quantity = xl.Quantity,
                        ProductId = xl.Product.ProductId,
                        Product = new ProductDto
                        {
                            ProductId = xl.ProductId,
                            ProductName = xl.Product.ProductName,
                            ProductPrice = xl.Product.ProductPrice
                        },
                        Total = xl.Total,
                    }).ToList()
                }).ToListAsync();


        return order;
    }

    public async Task<OrderModelDto> GetOrderById(Guid id)
    {
        var orderGet = await _orderdbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

        // _logger.LogInformation($"Order user : {JsonConvert.SerializeObject(orderGet)}");

        var orderLine = new OrderModelDto
        {
            Id = orderGet.Id,
            Address = orderGet.Address,
            UserId = orderGet.UserId,
            ZipCode = orderGet.ZipCode,
            EmailAddress = orderGet.EmailAddress,
            FirstName = orderGet.FirstName,
            LastName = orderGet.LastName,
            PhoneNumber = orderGet.PhoneNumber,
            OrderPaid = orderGet.OrderPaid,
            OrderPlaced = orderGet.OrderPlaced,
            OrderLines = orderGet.OrderLines.Select(x => new OrderLineDto
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Product = new ProductDto
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    ProductPrice = x.Product.ProductPrice
                },
                Total = x.Total
            }).ToList(),
            TotalPrice = orderGet.TotalPrice
        };

        _logger.LogInformation($"orderLine Get : {JsonConvert.SerializeObject(orderLine)}");
        return orderLine;
    }


    public bool CreateOrder(OrderModelDto orderModelDto)
    {
        try
        {
            var orderUser = new OrderModel
            {
                UserId = orderModelDto.UserId,
                EmailAddress = orderModelDto.EmailAddress,
                FirstName = orderModelDto.FirstName,
                LastName = orderModelDto.LastName,
                Address = orderModelDto.Address,
                ZipCode = orderModelDto.ZipCode,
                PhoneNumber = orderModelDto.PhoneNumber,
                OrderPaid = orderModelDto.OrderPaid,
                OrderPlaced = DateTime.UtcNow,
                TotalPrice = orderModelDto.TotalPrice,
                CreateBy = "admin",
                CreateTime = DateTime.UtcNow,
                LastModifiedDate = null,
                LastModifiedBy = "IDK",
            };

            _orderdbContext.Orders.Add(orderUser);
            _orderdbContext.SaveChanges();

            foreach (var item in orderModelDto.OrderLines)
            {
                // var getProduct = _orderdbContext.Products.FirstOrDefault(x => x.ProductId == item.ProductId);

                // if (getProduct != null)
                // {
                var productCheck = _productRepository.GetProduct(new ProductDto
                {
                    ProductId = item.Product.ProductId,
                    ProductName = item.Product.ProductName,
                    ProductPrice = item.Product.ProductPrice
                });

                var orderLineUser = new OrderLine
                {
                    Id = item.Id,
                    OrderModelId = orderUser.Id,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    Product = productCheck,
                    Total = item.Total,
                };
                // _logger.LogInformation($"orderline --->{JsonConvert.SerializeObject(orderLineUser)}");
                _orderdbContext.OrderLines.Add(orderLineUser);
                // }

                // else
                // {
                //     var orderLineUser = new OrderLine
                //     {
                //         Id = item.Id,
                //         Quantity = item.Quantity,
                //         OrderModelId = orderUser.Id,
                //         ProductId = item.Product.ProductId,
                //         Product = new Product
                //         {
                //             ProductId = item.Product.ProductId,
                //             ProductName = item.Product.ProductName,
                //             ProductPrice = item.Product.ProductPrice
                //         },
                //         Total = item.Total,
                //     };
                //     // _logger.LogInformation($"orderline --->{JsonConvert.SerializeObject(orderLineUser)}");
                //     _orderdbContext.OrderLines.Add(orderLineUser);
                // }
            }

            _orderdbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Model :{e.Message}");
            return false;
        }
    }
}