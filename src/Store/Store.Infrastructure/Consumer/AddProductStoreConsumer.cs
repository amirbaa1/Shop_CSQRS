using AutoMapper;
using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Application.Feature.Store.Commands.Create;
using Store.Domain.Repository;

namespace Store.Infrastructure.Consumer;

public class AddProductStoreConsumer : IConsumer<ProductStoreEvent>
{
    private readonly IStoreRespository _storeRespository;
    private readonly ILogger<AddProductStoreConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AddProductStoreConsumer(IStoreRespository storeRespository, ILogger<AddProductStoreConsumer> logger,
        IMediator mediator, IMapper mapper)
    {
        _storeRespository = storeRespository;
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ProductStoreEvent> context)
    {
        var message = context.Message;


        //var storeMessage = new CreateStoreCommand
        //{
        //    ProductId = message.ProductId,
        //    ProductName = message.ProductName,
        //    Price = message.Price,
        //    Number = message.Number,
        //    Status = MapProductStatusEventToProductStatus(message.ProductStatusEvent)
        //};

        var map = _mapper.Map<CreateStoreCommand>(message);
        
        _logger.LogInformation($"--->Add product in store ,consumer : {JsonConvert.SerializeObject(map)}");



        // _storeRespository.CreateStore(storeMessage);
        await _mediator.Send(map);

        await Task.CompletedTask; 
    }
    //// WT..!
    //// WT..!
    //private ProductStatus MapProductStatusEventToProductStatus(ProductStatusEvent productStatusEvent)
    //{
    //    return productStatusEvent switch
    //    {
    //        ProductStatusEvent.Available => ProductStatus.Available,
    //        ProductStatusEvent.OutOfStock => ProductStatus.OutOfStock,
    //        _ => throw new ArgumentOutOfRangeException(nameof(productStatusEvent), productStatusEvent, null)
    //    };
    //}
}