using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.Events;

public class EventCollectionRepository : IEventRepository
{
    private static readonly List<WebhookEvent> _events = [];
    
    public async Task<string> Create(WebhookEvent eventData)
    {
        if (string.IsNullOrWhiteSpace(eventData.Payload))
        {
            throw new ArgumentNullException("Payload cannot be null or empty.", nameof(eventData.Payload));
        }
        
        _events.Add(eventData);
        
        return await Task.FromResult(eventData.Id);
    }

    public async Task<WebhookEvent?> Get(string eventId)
    {
        return _events.FirstOrDefault(e => e.Id == eventId);
    }
}