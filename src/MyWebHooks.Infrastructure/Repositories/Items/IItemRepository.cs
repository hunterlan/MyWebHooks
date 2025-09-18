using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Items;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetItems();
    Task AddItem(Item item);
}