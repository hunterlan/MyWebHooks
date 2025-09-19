using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.SubscriptionEvents;

public interface ISubscriptionEventRepository
{
    void CreateMany(IEnumerable<WebhookSubscriptionEvent> subscriptionEvents);
}