using System.Net;

namespace Basket.Domain.Model.Dto;

public class ResultDto
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

public class ResultDto<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}