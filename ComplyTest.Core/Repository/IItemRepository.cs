using ComplyTest.Core.Entity;

namespace ComplyTest.Core.Repository;

public interface IItemRepository
{
    IQueryable<Item> GetItemList();
    Task<Item?> GetItemDetails(Guid id);
    Task<Item> AddItem(Item item);
    Task UpdateItem(Guid id, Item item);
    Task DeleteItem(Guid id);
}
