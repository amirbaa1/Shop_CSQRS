using System.Text.Json.Serialization;

namespace Contracts.Product
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductStatusRequest
    {
        Available = 1,
        OutOfStock = 0
    }
}