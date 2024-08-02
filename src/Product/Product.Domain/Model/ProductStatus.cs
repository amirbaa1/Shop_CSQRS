using System.Text.Json.Serialization;

namespace Product.Domain.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductStatus
    {
        Available = 1,
        OutOfStock = 0
    }
}