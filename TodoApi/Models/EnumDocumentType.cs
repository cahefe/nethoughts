using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumDocumentType : byte
    {
        Unknown = 0,
        RG = 1,
        CPF = 2,
        CNH = 3
    }
}