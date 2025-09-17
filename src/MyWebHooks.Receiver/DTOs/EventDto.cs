using MyWebHooks.Receiver.Models;

namespace MyWebHooks.Receiver.DTOs;

public record EventDto(SubEventType EventName, string Payload);