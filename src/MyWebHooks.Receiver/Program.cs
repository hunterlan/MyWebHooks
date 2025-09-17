using System.Net;
using MyWebHooks.Receiver.DTOs;

List<EventDto> events = [];
const string webhookUrlResponse = "https://localhost:7202/api/webhooks/item-add";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
var app = builder.Build();

await SubscribeToWebHook(app.Services, webhookUrlResponse);

app.MapGet("/api/events", () => events);

app.MapPost("/api/webhooks/item-add", (EventDto receivedEvent) =>
{
    events.Add(receivedEvent);
    return Results.Accepted();
});

app.Run();

static async Task SubscribeToWebHook(IServiceProvider serviceProvider, string url)
{
    var httpClient = serviceProvider.GetService<HttpClient>();
    if (httpClient is null)
    {
        throw new Exception("HttpClient is null");
    }

    WebhookSubscriptionDto webhookSubscription = new("app1", 0, url, null);
    using var response = await httpClient.PostAsJsonAsync("https://localhost:7201/api/subscriptions", webhookSubscription);
    if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Conflict)
    {
        throw new Exception("Can't subscribe to webhook");
    }
}