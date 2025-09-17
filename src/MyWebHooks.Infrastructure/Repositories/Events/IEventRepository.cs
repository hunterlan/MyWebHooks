using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Events;

public interface IEventRepository
{
    string Create(WebhookEvent eventData);
    WebhookEvent? Get(string eventId);
}