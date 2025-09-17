using MyWebHooks.Sender.DTOs;
using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.Services.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private readonly List<WebhookSubscription> _subscriptions = [];
    public string Create(WebhookSubscriptionDto subscription)
    {
        WebhookSubscription newSubscription = new()
        {
            Id = Guid.CreateVersion7().ToString(),
            CallbackUrl = subscription.CallbackUrl,
            EventType = subscription.EventType,
            Secret = subscription.Secret,
            UniqueName = subscription.UniqueName,
        };
        
        if (_subscriptions.Any(s => s.Equals(newSubscription)))
        {
            throw new ArgumentException("Subscription already exists");
        }
        
        _subscriptions.Add(newSubscription);
        return newSubscription.Id;
    }
}