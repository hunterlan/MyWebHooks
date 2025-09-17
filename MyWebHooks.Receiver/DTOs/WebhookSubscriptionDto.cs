namespace MyWebHooks.Receiver.DTOs;

public record WebhookSubscriptionDto(string UniqueName, int EventType, string CallbackUrl, string? Secret);