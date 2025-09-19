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
    
    public Task<IEnumerable<Item>> GetItemsAsync() => _itemRepository.GetItems();

    public Task AddItemAsync(Item item) => _itemRepository.AddItem(item);
}