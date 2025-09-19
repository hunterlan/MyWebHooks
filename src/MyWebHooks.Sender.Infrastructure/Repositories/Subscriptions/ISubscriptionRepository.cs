using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.Subscriptions;

public interface ISubscriptionRepository
{
    Task<IEnumerable<WebhookSubscription>> GetAllByEventType(SubEventType eventType);
    Task<string> Create(WebhookSubscription subscription);
}