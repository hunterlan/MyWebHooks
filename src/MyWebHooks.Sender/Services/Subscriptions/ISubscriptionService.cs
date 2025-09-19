using MyWebHooks.Sender.Infrastructure.Models;
using MyWebHooks.Sender.DTOs;

namespace MyWebHooks.Sender.Services.Subscriptions;

public interface ISubscriptionService
{
    Task<IEnumerable<WebhookSubscription>> GetAllByEventType(SubEventType eventType);
    Task<string> Create(WebhookSubscriptionDto subscription);
}