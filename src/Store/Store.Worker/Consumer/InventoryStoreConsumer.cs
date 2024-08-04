using AutoMapper;
using Contracts.Basket;
using Contracts.General;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Worker.Consumer;

public class InventoryStoreConsumer : IConsumer<SendToOrderRequest>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IStoreRepository _storeRepository;
    private readonly ILogger<InventoryStoreConsumer> _logger;

    public InventoryStoreConsumer(IMediator mediator, IMapper mapper, IStoreRepository storeRepository,
        ILogger<InventoryStoreConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _storeRepository = storeRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SendToOrderRequest> context)
    {
        var message = context.Message;
        // _logger.LogInformation($"---->{JsonConvert.SerializeObject(message)}");
        // var map = _mapper.Map<UpdateNumberDto>(message);
        // _logger.LogInformation($"--->{JsonConvert.SerializeObject(map)}");
        
        
        var map = new UpdateNumberDto
        {
            ProductId = message.BasketItems.FirstOrDefault()?.ProductId ?? Guid.Empty,
            Number = message.BasketItems.FirstOrDefault()?.Quantity ?? 0
        };
        
        var response = await _storeRepository.UpdateInventoryAfterPurchase(map);

        var result = new ResponseResult();
        result.IsSuccessful = response.IsSuccessful;
        result.Message = response.Message;
        result.StatusCode = response.StatusCode;

        await context.RespondAsync(result);
    }
}