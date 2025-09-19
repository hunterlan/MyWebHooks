namespace MyWebHooks.Infrastructure.Models;

public class WebhookSubscription
{
    public string Id { get; set; }
    public string UniqueName { get; set; }
    public SubEventType EventType { get; set; }
    public string CallbackUrl { get; set; }
    public string? Secret { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not WebhookSubscription subscription)
        {
            return false;
        }
        
        var subComp = subscription;
        if (subComp.UniqueName.Equals(subscription.UniqueName) && subComp.EventType.Equals(subscription.EventType)
                                                               && subComp.CallbackUrl.Equals(subscription.CallbackUrl))
        {
            return true;
        }

        return false;
    }
}