using System.Text.Json.Serialization;
using Product.Domain.Model;

namespace Contracts.Product
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductStatusRequest
    {
        Available = 1,
        OutOfStock = 0
    }
}