using MyWebHooks.Sender.Infrastructure.DAL;
using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.SubscriptionEvents;

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