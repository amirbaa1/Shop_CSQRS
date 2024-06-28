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
                    UserId = x.UserId,
                    TotalPrice = x.TotalPrice,
                    OrderPlaced = x.OrderPlaced,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    ZipCode = x.ZipCode,
                    EmailAddress = x.EmailAddress,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    OrderLines = x.OrderLines.Select(x => new OrderLineDto
                    {
                        Id = x.Id,
                        Quantity = x.Quantity,
                        Product = new ProductDto
                        {
                            ProductId = x.ProductId,
                            ProductName = x.Product.ProductName,
                            ProductPrice = x.Product.ProductPrice
                        },
                        Total = x.Total,
                    }).ToList()
                }).ToListAsync();

        return order;
    }

    public async Task<OrderModelDto> GetOrderById(Guid id)
    {
        var orderGet = await _orderdbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

        _logger.LogInformation($"Order user : {JsonConvert.SerializeObject(orderGet)}");

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

            foreach (var item in orderModelDto.OrderLines)
            {
                var getProduct = _orderdbContext.Products.FirstOrDefault(x => x.ProductId == item.ProductId);

                if (getProduct != null)
                {
                    var product = getProduct;

                    var orderLineUser = new OrderLine
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        ProductId = item.ProductId,
                        Total = item.Total,
                    };
                    _orderdbContext.OrderLines.Add(orderLineUser);
                }

                else
                {
                    var orderLineUser = new OrderLine
                    {
                        Id = item.Id,

                        Quantity = item.Quantity,
                        ProductId= item.ProductId,
                        Product = new Product
                        {
                            ProductId = item.Product.ProductId,
                            ProductName = item.Product.ProductName,
                            ProductPrice = item.Product.ProductPrice
                        },
                        Total = item.Total,
                    };

                    _orderdbContext.OrderLines.Add(orderLineUser);
                }
            }

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



            //save 
            //_logger.LogInformation($"--->2 order{JsonConvert.SerializeObject(order)}");
            //_orderdbContext.Orders.Add(order);
            //

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