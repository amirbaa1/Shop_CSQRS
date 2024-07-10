using AutoMapper;
using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Application.Feature.Store.Commands.Create;
using Store.Domain.Repository;

namespace Store.Application.Consumer;

public class AddProductStoreConsumer : IConsumer<ProductStoreEvent>
{
    private readonly IStoreRepository _storeRepository;
    private readonly ILogger<AddProductStoreConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AddProductStoreConsumer(IStoreRepository storeRepository, ILogger<AddProductStoreConsumer> logger,
        IMediator mediator, IMapper mapper)
    {
        _storeRepository = storeRepository;
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



        // _storeRepository.CreateStore(storeMessage);
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