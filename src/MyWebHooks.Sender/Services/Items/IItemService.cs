using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Sender.Services.Items;

public interface IItemService
{
    IEnumerable<Item> GetItems();
    void AddItem(Item item);
}