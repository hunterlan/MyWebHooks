using MyWebHooks.Receiver.Models;

namespace MyWebHooks.Receiver.DTOs;

public record EventDto(string Id, DateTime Timestamp, SubEventType EventType, string Payload);