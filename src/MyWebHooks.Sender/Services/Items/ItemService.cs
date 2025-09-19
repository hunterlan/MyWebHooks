using MyWebHooks.Sender.Infrastructure.Models;
using MyWebHooks.Sender.Infrastructure.Repositories.Items;

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