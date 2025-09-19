using MyWebHooks.Infrastructure.Models;
using MyWebHooks.Sender.DTOs;

namespace MyWebHooks.Sender.Services.Subscriptions;

public interface ISubscriptionService
{
    IEnumerable<WebhookSubscription> GetAllByEventType(SubEventType eventType);
    string Create(WebhookSubscriptionDto subscription);
}