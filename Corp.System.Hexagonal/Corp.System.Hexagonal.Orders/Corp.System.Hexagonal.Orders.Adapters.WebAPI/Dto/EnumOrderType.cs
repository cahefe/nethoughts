using System.Text.Json.Serialization;

namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI.Dto
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumOrderType : byte
    {
        Buy = 1,
        Sell = 2
    }
}
