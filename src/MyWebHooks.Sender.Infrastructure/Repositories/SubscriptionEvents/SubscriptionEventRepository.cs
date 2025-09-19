using MyWebHooks.Sender.Infrastructure.DAL;
using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.SubscriptionEvents;

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