using MyWebHooks.Infrastructure.DAL;
using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Items;

public class ItemRepository(MyDbContext context) : IItemRepository
{
    public IEnumerable<Item> GetItems()
    {
        return context.Item;
    }

    public void AddItem(Item item)
    {
        if (context.Item.Any(i => i.Id == item.Id))
        {
            throw new Exception("Item already exists");
        }
        context.Item.Add(item);
    }
}