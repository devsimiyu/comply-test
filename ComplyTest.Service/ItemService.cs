using ComplyTest.Core.Entity;
using ComplyTest.Core.Repository;
using ComplyTest.Core.Service;

namespace ComplyTest.Service;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;

    public ItemService(IItemRepository repository)
        => _repository = repository;

    public List<Item> GetItemList()
        => _repository.GetItemList().ToList();

    public async Task<Item?> GetItemDetails(Guid id)
        => await _repository.GetItemDetails(id);

    public async Task<Item> AddItem(Item item)
        => await _repository.AddItem(item);

    public async Task UpdateItem(Guid id, Item item)
        => await _repository.UpdateItem(id, item);

    public async Task DeleteItem(Guid id)
        => await _repository.DeleteItem(id);

    public Task CalculateFactorial(Stream stream)
    {
        throw new NotImplementedException();
    }
}
