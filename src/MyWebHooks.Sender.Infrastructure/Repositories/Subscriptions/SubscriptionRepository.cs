using Microsoft.EntityFrameworkCore;
using MyWebHooks.Sender.Infrastructure.DAL;
using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.Subscriptions;

public class SubscriptionRepository(MyDbContext context) : ISubscriptionRepository
{
    public async Task<IEnumerable<WebhookSubscription>> GetAllByEventType(SubEventType eventType)
    {
        return await context.WebhookSubscription.Where(s => s.EventType == eventType).ToListAsync();        
    }

    public async Task<string> Create(WebhookSubscription subscription)
    {
        var idExists = context.WebhookSubscription.Any(s => s.Id == subscription.Id);

        if (idExists)
        {
            throw new ArgumentException("Subscription already exists");
        }
        
        await context.WebhookSubscription.AddAsync(subscription);
        await context.SaveChangesAsync();
        return subscription.Id;
    }
}