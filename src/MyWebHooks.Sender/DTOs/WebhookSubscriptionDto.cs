using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.DTOs;

public record WebhookSubscriptionDto(string UniqueName, SubEventType EventType, string CallbackUrl, string? Secret);