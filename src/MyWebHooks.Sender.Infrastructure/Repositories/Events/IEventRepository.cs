using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.Events;

public interface IEventRepository
{
    Task<string> Create(WebhookEvent eventData);
    Task<WebhookEvent?> Get(string eventId);
}