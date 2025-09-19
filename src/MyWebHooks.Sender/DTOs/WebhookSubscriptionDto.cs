using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.DTOs;

public record WebhookSubscriptionDto(string UniqueName, SubEventType EventType, string CallbackUrl, string? Secret);