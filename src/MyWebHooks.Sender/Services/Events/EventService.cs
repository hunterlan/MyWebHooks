using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.Services.Events;

public class EventService : IEventService
{
    private const int StartRetryDelaySec = 3;
    private const int MaxRetries = 3;
    private const int MaxTimeout = 10;
    private readonly List<WebhookEvent> _events = [];
    private readonly List<WebhookSubscriptionEvent> _subscriptionEvents = [];
    private readonly HttpClient _httpClient;

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(MaxTimeout);
    }
    
    public string Create(string payload, SubEventType type)
    {
        if (string.IsNullOrWhiteSpace(payload))
        {
            throw new ArgumentException("Payload cannot be null or empty.", nameof(payload));
        }

        var newEvent = new WebhookEvent
        {
            Id = Guid.CreateVersion7().ToString(),
            Payload = payload,
            Type = type,
            Timestamp = DateTime.UtcNow,
        };
        
        _events.Add(newEvent);
        return newEvent.Id;
    }

    public async Task<IEnumerable<WebhookSubscriptionEvent>> SendAsync(string eventId,
        IEnumerable<WebhookSubscription> subscriptions)
    {
        var newSubEvents = new List<WebhookSubscriptionEvent>();
        if (string.IsNullOrWhiteSpace(eventId))
        {
            throw new ArgumentException("Id cannot be null or empty.", nameof(eventId));
        }

        var @event = _events.FirstOrDefault(e => e.Id == eventId);
        
        if (@event is null)
        {
            throw new ArgumentException($"Event with id {eventId} does not exist.", nameof(eventId));
        }

        foreach (var subscription in subscriptions)
        {
            if (subscription.EventType != @event.Type) continue;
            
            WebhookSubscriptionEvent newSubscriptionEvent = new()
            {
                EventId = eventId,
                SubscriptionId = subscription.Id,
                RetryCount = 0,
                IsSuccessful = true
            };
                
            var retryCount = 0;
            var retryDelaySeconds = StartRetryDelaySec;
            while (retryCount < MaxRetries)
            {
                using HttpResponseMessage response = await _httpClient.PostAsync(subscription.CallbackUrl, new StringContent(@event.Payload));
                if (response.IsSuccessStatusCode)
                {
                    break;
                }

                retryCount++;
                retryDelaySeconds *= retryCount;
                Thread.Sleep(retryDelaySeconds * 1000);
            }

            newSubscriptionEvent.RetryCount = retryCount;
            if (retryCount >= MaxRetries)
            {
                newSubscriptionEvent.IsSuccessful = false;
            }
            
            newSubEvents.Add(newSubscriptionEvent);
        }
        
        _subscriptionEvents.AddRange(newSubEvents);
        return newSubEvents;
    }
}