using MyWebHooks.Sender.DTOs;

namespace MyWebHooks.Sender.Services.Subscriptions;

public interface ISubscriptionService
{
    string Create(WebhookSubscriptionDto subscription);
}