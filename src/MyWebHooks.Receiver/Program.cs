using System.Net;
using MyWebHooks.Receiver.DTOs;
using Serilog;

List<EventDto> events = [];
const string webhookUrlResponse = "https://localhost:7202/api/webhooks/item-add";
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog((services, lc) =>
    {
        lc.ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext();
    });
    builder.Services.AddHttpClient();
    var app = builder.Build();

    await SubscribeToWebHook(app.Services, webhookUrlResponse);

    app.MapGet("/api/events", () => events);

    app.MapPost("/api/webhooks/item-add", (EventDto receivedEvent, ILogger<Program> logger) =>
    {
        events.Add(receivedEvent);
        logger.LogInformation("Received event {eventType}", receivedEvent.EventType);
        return Results.Accepted();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"Application start-up failed. Reason: {ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}

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

    Log.Information("Subscribed to webhook.");
}