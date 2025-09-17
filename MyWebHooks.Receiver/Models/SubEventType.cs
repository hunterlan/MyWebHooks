using System.Text.Json.Serialization;

namespace MyWebHooks.Receiver.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubEventType
{
    ItemAdded,
    ItemUpdated
}