using MyWebHooks.Infrastructure.Models;
using MyWebHooks.Infrastructure.Repositories.Subscriptions;
using MyWebHooks.Sender.DTOs;

namespace MyWebHooks.Sender.Services.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private ISubscriptionRepository  _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public IEnumerable<WebhookSubscription> GetAllByEventType(SubEventType eventType) => 
        _subscriptionRepository.GetAllByEventType(eventType);

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
        
        var id = _subscriptionRepository.Create(newSubscription);
        return id;
    }
}