using MyWebHooks.Infrastructure.DAL;
using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.SubscriptionEvents;

public class SubscriptionEventRepository(MyDbContext context) :  ISubscriptionEventRepository
{
    public void CreateMany(IEnumerable<WebhookSubscriptionEvent> subscriptionEvents)
    {
        foreach (var subscriptionEvent in subscriptionEvents)
        {
            context.WebhookSubscriptionEvent.Add(subscriptionEvent);
        }

        context.SaveChanges();
    }
}