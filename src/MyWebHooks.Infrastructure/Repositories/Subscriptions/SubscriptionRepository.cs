using MyWebHooks.Infrastructure.DAL;
using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Subscriptions;

public class SubscriptionRepository(MyDbContext context) : ISubscriptionRepository
{
    public IEnumerable<WebhookSubscription> GetAllByEventType(SubEventType eventType)
    {
        return context.WebhookSubscription.Where(s => s.EventType == eventType);        
    }

    public string Create(WebhookSubscription subscription)
    {
        var idExists = context.WebhookSubscription.Any(s => s.Id == subscription.Id);

        if (idExists)
        {
            throw new ArgumentException("Subscription already exists");
        }
        
        context.WebhookSubscription.Add(subscription);
        context.SaveChanges();
        return subscription.Id;
    }

    public void CreateMany(IEnumerable<WebhookSubscription> subscriptions)
    {
        foreach (var subscription in subscriptions)
        {
            var idExists = context.WebhookSubscription.Any(s => s.Id == subscription.Id);

            if (idExists)
            {
                throw new ArgumentException("Subscription already exists");
            }
        
            context.WebhookSubscription.Add(subscription);
        }

        context.SaveChanges();
    }
}