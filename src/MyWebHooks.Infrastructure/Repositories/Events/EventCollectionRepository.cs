using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Events;

public class EventCollectionRepository : IEventRepository
{
    private static readonly List<WebhookEvent> _events = [];
    
    public string Create(WebhookEvent eventData)
    {
        if (string.IsNullOrWhiteSpace(eventData.Payload))
        {
            throw new ArgumentNullException("Payload cannot be null or empty.", nameof(eventData.Payload));
        }
        
        _events.Add(eventData);
        
        return eventData.Id;
    }

    public WebhookEvent? Get(string eventId)
    {
        return  _events.FirstOrDefault(e => e.Id == eventId);
    }
}