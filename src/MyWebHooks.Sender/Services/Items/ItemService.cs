using MyWebHooks.Infrastructure.Models;
using MyWebHooks.Infrastructure.Repositories.Items;

namespace MyWebHooks.Sender.Services.Items;

public class ItemService : IItemService
{
    private IItemRepository _itemRepository;
    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    
    public IEnumerable<Item> GetItems() => _itemRepository.GetItems();

    public void AddItem(Item item)
    {
        _itemRepository.AddItem(item);
    }
}