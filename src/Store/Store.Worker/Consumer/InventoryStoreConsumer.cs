using AutoMapper;
using Contracts.Basket;
using Contracts.General;
using MassTransit;
using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Worker.Consumer;

public class InventoryStoreConsumer : IConsumer<CheckOutStoreRequest>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IStoreRepository _storeRepository;

    public InventoryStoreConsumer(IMediator mediator, IMapper mapper, IStoreRepository storeRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _storeRepository = storeRepository;
    }

    public async Task Consume(ConsumeContext<CheckOutStoreRequest> context)
    {
        var message = context.Message;
        var map = _mapper.Map<UpdateNumberDto>(message);
        var response = await _storeRepository.UpdateInventoryAfterPurchase(map);

        var result = new ResponseResult();
        result.IsSuccessful = response.IsSuccessful;
        result.Message = response.Message;
        result.StatusCode = response.StatusCode;

        await context.RespondAsync(result);
    }
}