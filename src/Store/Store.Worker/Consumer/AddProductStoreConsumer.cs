using AutoMapper;
using Contracts.General;
using Contracts.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Api.Feature.Store.Commands.Create;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Worker.Consumer;

// public class AddProductStoreConsumer : IConsumer<ProductStoreEvent>
public class AddProductStoreConsumer : IConsumer<ProductAddStoreRequest>
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

    // public async Task Consume(ConsumeContext<ProductStoreEvent> context)
    // {
    //     var message = context.Message;
    //
    //
    //     //var storeMessage = new CreateStoreCommand
    //     //{
    //     //    ProductId = message.ProductId,
    //     //    ProductName = message.ProductName,
    //     //    Price = message.Price,
    //     //    Number = message.Number,
    //     //    Status = MapProductStatusEventToProductStatus(message.ProductStatusEvent)
    //     //};
    //
    //     var map = _mapper.Map<CreateStoreCommand>(message);
    //     
    //     _logger.LogInformation($"--->Add product in store ,consumer : {JsonConvert.SerializeObject(map)}");
    //
    //
    //
    //     // _storeRepository.CreateStore(storeMessage);
    //     await _mediator.Send(map);
    //
    //     await Task.CompletedTask; 
    // }

    public async Task Consume(ConsumeContext<ProductAddStoreRequest> context)
    {
        var message = context.Message;

        //var map = _mapper.Map<CreateStoreCommand>(message);

        //_logger.LogInformation($"--->Add product in store ,consumer : {JsonConvert.SerializeObject(map)}");

        //await _mediator.Send(map);

        var map = _mapper.Map<StoreDto>(message);


        var create = await _storeRepository.CreateStore(map);

        var result = new ResponseResult();
        var store = await _storeRepository.GetStoreByProductId(context.Message.ProductId);
        if (store == null)
        {
            result.Message = "No save in store";
            result.IsSuccessful = false;
            await context.RespondAsync(result);
        }

        result.Message = "save in store";
        result.IsSuccessful = true;
        await context.RespondAsync(result);
    }
}