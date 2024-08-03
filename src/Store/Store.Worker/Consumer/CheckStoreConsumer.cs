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

public class CheckStoreConsumer : IConsumer<CheckOutStoreRequest>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckStoreConsumer> _logger;
    private readonly IStoreRepository _storeRepository;

    public CheckStoreConsumer(IMediator mediator, IMapper mapper, ILogger<CheckStoreConsumer> logger,
        IStoreRepository storeRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
        _storeRepository = storeRepository;
    }

    public async Task Consume(ConsumeContext<CheckOutStoreRequest> context)
    {
        // var map = _mapper.Map<CheckStoreQuery>(context.Message);
        // _logger.LogInformation($"---> {JsonConvert.SerializeObject(map)}");
        // await _mediator.Send(map);
        var map = _mapper.Map<CheckNumberDto>(context.Message);
        var response = await _storeRepository.CheckStore(map);

        var result = new ResponseResult();

        result.IsSuccessful = response.IsSuccessful;
        result.Message = response.Message;
        result.StatusCode = response.StatusCode;

        _logger.LogInformation($"---> Response {JsonConvert.SerializeObject(result)}");

        await context.RespondAsync(result);
    }
}