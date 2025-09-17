using MyWebHooks.Sender.DTOs;
using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.Services.Subscriptions;

public interface ISubscriptionService
{
    IEnumerable<WebhookSubscription> GetAllByEventType(SubEventType eventType);
    string Create(WebhookSubscriptionDto subscription);
}