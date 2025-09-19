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

    public Task<IEnumerable<WebhookSubscription>> GetAllByEventType(SubEventType eventType) => 
        _subscriptionRepository.GetAllByEventType(eventType);

    public Task<string> Create(WebhookSubscriptionDto subscription)
    {
        WebhookSubscription newSubscription = new()
        {
            Id = Guid.CreateVersion7().ToString(),
            CallbackUrl = subscription.CallbackUrl,
            EventType = subscription.EventType,
            Secret = subscription.Secret,
            UniqueName = subscription.UniqueName,
        };
        
        return _subscriptionRepository.Create(newSubscription);
    }
}