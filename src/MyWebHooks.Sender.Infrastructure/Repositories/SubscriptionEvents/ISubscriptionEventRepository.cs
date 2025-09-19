using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.SubscriptionEvents;

public interface ISubscriptionEventRepository
{
    void CreateMany(IEnumerable<WebhookSubscriptionEvent> subscriptionEvents);
}