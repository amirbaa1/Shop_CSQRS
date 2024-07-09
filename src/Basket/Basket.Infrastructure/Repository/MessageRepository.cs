using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<MessageRepository> _logger;

    public MessageRepository(IDistributedCache distributedCache, ILogger<MessageRepository> logger)
    {
        _distributedCache = distributedCache;
        _logger = logger;
    }

    public async Task<bool> MessageResultSet(ResultDto resultDto)
    {
        await _distributedCache.SetStringAsync(resultDto.ProductId.ToString(), JsonConvert.SerializeObject(resultDto));
        return true;
    }

    public async Task<ResultDto> GetMessageResult(string productId)
    {
        var result = await _distributedCache.GetStringAsync(productId);

        if (string.IsNullOrEmpty(result))
        {
            return null; // اگر نتیجه خالی یا null بود، مقدار null برگردانید
        }

        var resultDto = JsonConvert.DeserializeObject<ResultDto>(result!);

        _logger.LogInformation($"---->message resultdto : {JsonConvert.SerializeObject(resultDto)} ");

        await RemoveMessage(productId);
        return resultDto!;
    }

    private async Task RemoveMessage(string productId)
    {
        await _distributedCache.RemoveAsync(productId);
        await Task.CompletedTask;
    }
}