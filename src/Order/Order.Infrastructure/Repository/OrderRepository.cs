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

    public OrderRepository(OrderdbContext orderdbContext, ILogger<OrderRepository> logger)
    {
        _orderdbContext = orderdbContext;
        _logger = logger;
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
                    // ItemCount = x.OrderLines.Count(),
                    TotalPrice = x.TotalPrice,
                    OrderPlaced = x.OrderPlaced,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    OrderLines = x.OrderLines.Select(x => new OrderLineDto
                    {
                        Id = x.Id,
                        Product = new ProductDto
                        {
                            ProductId = x.ProductId,
                            ProductName = x.Product.ProductName,
                            ProductPrice = x.Product.ProductPrice
                        },
                    }).ToList()
                }).ToListAsync();

        return order;
    }

    public async Task<OrderModelDto> GetOrderById(Guid id)
    {
        var orderGet = await _orderdbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        var orderLine = new OrderModelDto
        {
            Id = orderGet.Id,
            Address = orderGet.Address,
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
                ProductId = x.ProductId,
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
        return orderLine;
    }

    // public Task<List<OrderModelDto>> GetAll()
    // {
    //     throw new NotImplementedException();
    // }

    public OrderModelDto CreateOrder(OrderModelDto orderModelDto)
    {
        _logger.LogInformation($"Order MOdel --->{JsonConvert.SerializeObject(orderModelDto)}");
        var productID = new Product
        {
            ProductId = Guid.NewGuid(),
        };
        var order = new OrderModel
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
            OrderLines = orderModelDto.OrderLines.Select(ol => new OrderLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = ol.ProductId,
                    Quantity = ol.Quantity,
                    Product = new Product
                    {
                        // ProductId = ol.Product.ProductId,
                        ProductId = Guid.NewGuid(),
                        ProductName = ol.Product.ProductName,
                        ProductPrice = ol.Product.ProductPrice
                    },
                }
            ).ToList()
        };

        _logger.LogInformation($"---> order{JsonConvert.SerializeObject(order)}");
        _orderdbContext.Orders.Add(order);

        _orderdbContext.SaveChanges();

        return orderModelDto;
    }
}