using System.Net;

namespace Contracts.General;

public class ResponseResult
{
    public string Message { get; set; }
    public bool IsSuccessful { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}