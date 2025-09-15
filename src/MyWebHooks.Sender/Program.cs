using MyWebHooks.Sender.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IItemService, ItemService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();