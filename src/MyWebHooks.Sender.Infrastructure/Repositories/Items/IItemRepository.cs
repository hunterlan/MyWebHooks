using MyWebHooks.Sender.Infrastructure.Models;

namespace MyWebHooks.Sender.Infrastructure.Repositories.Items;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetItems();
    Task AddItem(Item item);
}