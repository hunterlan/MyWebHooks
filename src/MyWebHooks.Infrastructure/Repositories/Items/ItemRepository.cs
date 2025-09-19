using Microsoft.EntityFrameworkCore;
using MyWebHooks.Infrastructure.DAL;
using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.Repositories.Items;

public class ItemRepository(MyDbContext context) : IItemRepository
{
    public async Task<IEnumerable<Item>> GetItems()
    {
        return await context.Item.ToListAsync();
    }

    public async Task AddItem(Item item)
    {
        if (context.Item.Any(i => i.Id == item.Id))
        {
            throw new Exception("Item already exists");
        }
        await context.Item.AddAsync(item);
        await context.SaveChangesAsync();
    }
}