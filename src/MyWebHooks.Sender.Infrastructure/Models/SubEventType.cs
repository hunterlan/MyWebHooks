using System.Text.Json.Serialization;

namespace MyWebHooks.Sender.Infrastructure.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubEventType
{
    ItemAdded,
    ItemUpdated
}