using System.Net;

namespace Basket.Domain.Model.Dto;

public class ResultDto
{
    public Guid? ProductId { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
}

public class ResultDto<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}