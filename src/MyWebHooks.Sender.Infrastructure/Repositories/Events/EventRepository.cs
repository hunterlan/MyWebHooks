using Microsoft.EntityFrameworkCore;
using MyWebHooks.Sender.Infrastructure.DAL;
using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.Events;

public class EventRepository(MyDbContext context) : IEventRepository
{
    public async Task<string> Create(WebhookEvent eventData)
    {
        if (string.IsNullOrWhiteSpace(eventData.Payload))
        {
            throw new ArgumentNullException("Payload cannot be null or empty.", nameof(eventData.Payload));
        }
        
        await context.WebhookEvent.AddAsync(eventData);
        await context.SaveChangesAsync();
        return eventData.Id;
    }

    public async Task<WebhookEvent?> Get(string eventId)
    {
        return await context.WebhookEvent.FirstOrDefaultAsync(e => e.Id == eventId);
    }
}