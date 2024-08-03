using AutoMapper;
using Contracts.General;
using Contracts.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Worker.Consumer;

public class UpdateProductStoreConsumer : IConsumer<UpdateProductRequest>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UpdateProductStoreConsumer> _logger;
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;

    public UpdateProductStoreConsumer(IMediator mediator, ILogger<UpdateProductStoreConsumer> logger,
        IStoreRepository storeRepository, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _storeRepository = storeRepository;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<UpdateProductRequest> context)
    {
        var message = context.Message;
        // var updateMessage = new UpdateProductNameCommand
        // {
        //     Id = message.ProductId,
        //     Name = message.ProductName,
        //     Price = message.ProductPrice,
        // };
        //
        // _logger.LogInformation($"--->{JsonConvert.SerializeObject(updateMessage)}");
        //
        // await _mediator.Send(updateMessage);

        // var result = new ResponseResult
        // {
        //     IsSuccessful = true,
        //     Message = "Update product in store"
        // };

        var map = _mapper.Map<UpdateProductNameDto>(message);
        
        var response = await _storeRepository.UpdateProductName(map);

        var result = new ResponseResult
        {
            IsSuccessful = response.IsSuccessful,
            Message = response.Message,
            StatusCode = response.StatusCode
        };

        await context.RespondAsync(result);
    }
}