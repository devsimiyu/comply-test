using ComplyTest.Core.Entity;
using ComplyTest.Core.Repository;
using ComplyTest.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ComplyTest.Data.Repository;

public class ItemRepository : IItemRepository
{
    private readonly PersistenceContext _persistence;

    public ItemRepository(PersistenceContext persistence)
        => _persistence = persistence;

    public IQueryable<Item> GetItemList()
        => _persistence.Items.AsNoTracking();
    
    public async Task<Item?> GetItemDetails(Guid id)
        => await _persistence.Items.FindAsync(id);
    
    public async Task<Item> AddItem(Item item)
    {
        item.DateCreated = DateTime.UtcNow;
        
        await _persistence.Items.AddAsync(item);
        await _persistence.SaveChangesAsync();
        
        return item;
    }

    public async Task UpdateItem(Guid id, Item item)
    {
        using var transaction = await _persistence.Database.BeginTransactionAsync();

        try
        {
            await _persistence.Items
                .Where(x => x.Id.Equals(id))
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Name, item.Name)
                    .SetProperty(x => x.DateModified, DateTime.UtcNow));

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteItem(Guid id)
    {
        using var transaction = await _persistence.Database.BeginTransactionAsync();

        try
        {
            await _persistence.Items
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
                
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
