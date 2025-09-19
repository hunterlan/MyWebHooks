using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Subscriptions;

public interface ISubscriptionRepository
{
    Task<IEnumerable<WebhookSubscription>> GetAllByEventType(SubEventType eventType);
    Task<string> Create(WebhookSubscription subscription);
}