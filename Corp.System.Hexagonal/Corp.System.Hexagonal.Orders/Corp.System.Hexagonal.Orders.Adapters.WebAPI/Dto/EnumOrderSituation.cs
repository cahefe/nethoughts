using System.Text.Json.Serialization;

namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI.Dto
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumOrderSituation : byte
    {
        Unknown = 0,
        Registered = 1,
        Approved = 2,
        Reproved = 3,
        Canceled = 4,
        Settled = 5
    }
}
