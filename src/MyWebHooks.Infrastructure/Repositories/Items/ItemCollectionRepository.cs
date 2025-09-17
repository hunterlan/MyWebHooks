using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Items;

public class ItemCollectionRepository : IItemRepository
{
    private static readonly List<Item> _items = [];
    
    public IEnumerable<Item> GetItems()
    {
        return _items;        
    }

    public void AddItem(Item item)
    {
        if (_items.Any(i => i.Id == item.Id))
        {
            throw new Exception("Item already exists");
        }
        
        _items.Add(item);
    }
}