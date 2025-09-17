using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Sender.DTOs;

public record EventDto(SubEventType EventName, string Payload);