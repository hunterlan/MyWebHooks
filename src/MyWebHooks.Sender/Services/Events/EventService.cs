using MyWebHooks.Infrastructure.Models;
using MyWebHooks.Infrastructure.Repositories.Events;
using MyWebHooks.Infrastructure.Repositories.SubscriptionEvents;
using MyWebHooks.Infrastructure.Repositories.Subscriptions;
using MyWebHooks.Sender.DTOs;

namespace MyWebHooks.Sender.Services.Events;

public class EventService : IEventService
{
    private const int StartRetryDelaySec = 3;
    private const int MaxRetries = 3;
    private const int MaxTimeout = 10;
    private readonly HttpClient _httpClient;
    private readonly IEventRepository _eventRepository;
    private readonly ISubscriptionEventRepository  _subscriptionEventRepository;

    public EventService(HttpClient httpClient, IEventRepository eventRepository, ISubscriptionEventRepository subscriptionEventRepository)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(MaxTimeout);
        _eventRepository = eventRepository;
        subscriptionEventRepository = subscriptionEventRepository;
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
        
        _eventRepository.Create(newEvent);
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

        var @event = _eventRepository.Get(eventId);
        
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
                var eventDto = new EventDto(subscription.EventType, @event.Payload);
                using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(subscription.CallbackUrl, eventDto);
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
        
        _subscriptionEventRepository.CreateMany(newSubEvents);
        return newSubEvents;
    }
}