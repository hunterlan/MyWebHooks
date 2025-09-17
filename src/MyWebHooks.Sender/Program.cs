using System.Threading.Channels;
using MyWebHooks.Sender.Services;
using MyWebHooks.Sender.Services.Events;
using MyWebHooks.Sender.Services.Items;
using MyWebHooks.Sender.Services.Subscriptions;
using Serilog;

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
    
    builder.Services.AddHostedService<BackgroundSenderService>();

    builder.Services.AddSingleton<Channel<SenderChannelRequest>>(_ =>
        Channel.CreateBounded<SenderChannelRequest>(new BoundedChannelOptions(50)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            AllowSynchronousContinuations = false
        }));
    
    builder.Services.AddSingleton<IItemService, ItemService>();
    builder.Services.AddSingleton<ISubscriptionService, SubscriptionService>();
    builder.Services.AddSingleton<IEventService, EventService>();
    builder.Services.AddControllers();
    builder.Services.AddHttpClient();

    var app = builder.Build();

    app.MapControllers();

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
