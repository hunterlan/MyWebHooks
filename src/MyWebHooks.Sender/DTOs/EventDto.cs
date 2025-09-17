using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.DTOs;

public record EventDto(SubEventType EventName, string Payload);