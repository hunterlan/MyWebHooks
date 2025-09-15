using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.Services;

public interface IItemService
{
    IEnumerable<Item> GetItems();
    void AddItem(Item item);
}