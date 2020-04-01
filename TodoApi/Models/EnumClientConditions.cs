using System;
using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Flags]
    public enum EnumClientConditions : byte {
        None = 0,
        Employee = 1,
        Marryied = 2,
        Children = 4,
        Relatives = 8
    }
}