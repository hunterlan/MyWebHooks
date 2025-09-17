namespace MyWebHooks.Sender.Models;

public class WebhookEvent
{
    public string Id { get; set; }
    public DateTime Timestamp { get; set; }
    public SubEventType Type { get; set; }
    public string Payload { get; set; }
}