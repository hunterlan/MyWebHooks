using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Events;

public interface IEventRepository
{
    Task<string> Create(WebhookEvent eventData);
    Task<WebhookEvent?> Get(string eventId);
}