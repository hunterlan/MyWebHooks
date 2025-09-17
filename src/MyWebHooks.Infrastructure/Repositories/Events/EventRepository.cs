using MyWebHooks.Infrastructure.DAL;
using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Events;

public class EventRepository(MyDbContext context) : IEventRepository
{
    public string Create(WebhookEvent eventData)
    {
        if (string.IsNullOrWhiteSpace(eventData.Payload))
        {
            throw new ArgumentNullException("Payload cannot be null or empty.", nameof(eventData.Payload));
        }
        
        context.WebhookEvent.Add(eventData);
        context.SaveChanges();
        return eventData.Id;
    }

    public WebhookEvent? Get(string eventId)
    {
        return context.WebhookEvent.FirstOrDefault(e => e.Id == eventId);
    }
}