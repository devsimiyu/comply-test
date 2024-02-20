using ComplyTest.Core.Entity;

namespace ComplyTest.Core.Service;

public interface IItemService
{
    List<Item> GetItemList();
    Task<Item?> GetItemDetails(Guid id);
    Task<Item> AddItem(Item item);
    Task UpdateItem(Guid id, Item item);
    Task DeleteItem(Guid id);
    Task CalculateFactorial(Stream stream);
}
