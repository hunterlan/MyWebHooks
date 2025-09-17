using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Items;

public interface IItemRepository
{
    IEnumerable<Item> GetItems();
    void AddItem(Item item);
}