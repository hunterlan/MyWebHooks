using MyWebHooks.Sender.Models;

namespace MyWebHooks.Sender.Services;

public class ItemService : IItemService
{
    private readonly List<Item> _items = [];
    
    public IEnumerable<Item> GetItems() => _items;

    public void AddItem(Item item)
    {
        if (_items.FirstOrDefault(i => i.Id == item.Id) != null)
        {
            throw new Exception("Item already exists");
        }
        
        _items.Add(item);
    }
}