using MyWebHooks.Sender.DTOs;
using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.Services.Events;

public interface IEventService
{
    string Create(string payload, SubEventType type);
    Task<IEnumerable<WebhookSubscriptionEvent>> SendAsync(string eventId, IEnumerable<WebhookSubscription> subscriptions);
}