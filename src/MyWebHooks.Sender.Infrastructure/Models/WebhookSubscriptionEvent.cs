namespace MyWebHooks.Sender.Infrastructure.Models;

public class WebhookSubscriptionEvent
{
    public string SubscriptionId { get; set; }
    public string EventId { get; set; }
    public DateTime SendAt { get; set; }
    public bool IsSuccessful { get; set; }
    public int RetryCount { get; set; }
}