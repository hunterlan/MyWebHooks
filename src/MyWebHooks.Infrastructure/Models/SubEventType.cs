using System.Text.Json.Serialization;

namespace MyWebHooks.Infrastructure.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubEventType
{
    ItemAdded,
    ItemUpdated
}