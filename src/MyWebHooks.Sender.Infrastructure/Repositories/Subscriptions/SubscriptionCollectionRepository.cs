using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.Subscriptions;

public class SubscriptionCollectionRepository : ISubscriptionRepository
{
    private static readonly List<WebhookSubscription> _subscriptions = [];
    
    public async Task<IEnumerable<WebhookSubscription>> GetAllByEventType(SubEventType eventType)
    {
        return _subscriptions.Where(s => s.EventType == eventType);
    }

    public async Task<string> Create(WebhookSubscription subscription)
    {
        if (_subscriptions.Any(s => s.Id == subscription.Id))
        {
            throw new ArgumentException("Subscription already exists");
        }
        
        _subscriptions.Add(subscription);
        return subscription.Id;
    }
}