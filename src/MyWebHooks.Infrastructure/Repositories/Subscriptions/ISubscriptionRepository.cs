using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Subscriptions;

public interface ISubscriptionRepository
{
    public IEnumerable<WebhookSubscription> GetAllByEventType(SubEventType eventType);
    string Create(WebhookSubscription subscription);
}