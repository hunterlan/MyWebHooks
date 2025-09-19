using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Sender.Services.Items;

public interface IItemService
{
    Task<IEnumerable<Item>> GetItemsAsync();
    Task AddItemAsync(Item item);
}