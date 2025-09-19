using MyWebHooks.Infrastructure.DAL;
using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.SubscriptionEvents;

public class SubscriptionEventCollectionRepository :  ISubscriptionEventRepository
{
    private static readonly List<WebhookSubscriptionEvent> _events = [];
    
    public void CreateMany(IEnumerable<WebhookSubscriptionEvent> subscriptionEvents)
    {
        foreach (var subscriptionEvent in subscriptionEvents)
        {
            _events.Add(subscriptionEvent);
        }
    }
}