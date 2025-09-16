using MyWebHooks.Sender.Services;
using MyWebHooks.Sender.Services.Items;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IItemService, ItemService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();