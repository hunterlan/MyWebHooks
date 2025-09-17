using Microsoft.EntityFrameworkCore;
using MyWebHooks.Infrastructure.DAL;
using MyWebHooks.Infrastructure.Repositories.Events;
using MyWebHooks.Infrastructure.Repositories.Items;
using MyWebHooks.Infrastructure.Repositories.SubscriptionEvents;
using MyWebHooks.Infrastructure.Repositories.Subscriptions;
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
    
    var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase");

    if (connectionString != null && connectionString.Length > 0)
    {
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(connectionString)
        );
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        builder.Services.AddScoped<ISubscriptionEventRepository, SubscriptionEventRepository>();
    }
    else
    {
        builder.Services.AddScoped<IItemRepository, ItemCollectionRepository>();
        builder.Services.AddScoped<IEventRepository, EventCollectionRepository>();
        builder.Services.AddScoped<ISubscriptionRepository, SubscriptionCollectionRepository>();
        builder.Services.AddScoped<ISubscriptionEventRepository, SubscriptionEventCollectionRepository>();
    }

    builder.Services.AddScoped<IItemService, ItemService>();
    builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
    builder.Services.AddScoped<IEventService, EventService>();
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
