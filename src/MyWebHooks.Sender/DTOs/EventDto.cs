using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Sender.DTOs;

public record EventDto(string Id, DateTime Timestamp, SubEventType EventType, string Payload);