using System.Threading.Channels;
using MyWebHooks.Sender.DTOs;
using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.Services;

public class BackgroundSenderService : BackgroundService
{
    private readonly Channel<SenderChannelRequest> _channel;
    private readonly ILogger<BackgroundSenderService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public BackgroundSenderService(Channel<SenderChannelRequest> channel, ILogger<BackgroundSenderService> logger,
        IHttpClientFactory httpClientFactory)
    {
        _channel = channel;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            var httpClient = _httpClientFactory.CreateClient();
            var request = await _channel.Reader.ReadAsync(stoppingToken);
            
            _logger.LogInformation("Sending event {eventType} for subscriber {uniqueName} at {time}.", request.TriggeredEvent.EventType, 
                request.Subscription.UniqueName, DateTimeOffset.Now);
            
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(request.Subscription.CallbackUrl,
                request.TriggeredEvent, cancellationToken: stoppingToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Subscriber {id}:{uniqueName} didn't successfully received a message, event {eventId}.", 
                    request.Subscription.Id, request.Subscription.UniqueName, request.TriggeredEvent.Id);
            }
        }
    }
}

public record SenderChannelRequest(WebhookSubscription Subscription, EventDto TriggeredEvent);