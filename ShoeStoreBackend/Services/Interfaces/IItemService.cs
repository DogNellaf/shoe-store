using ShoeStore.Dto.Item;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IItemService
    {
        Item Create(ItemCreateDto dto, Shop shop);
        Item? Find(long id);
        void SetStorageCount(Item item, int storageCount);
        List<Item> FindMany(Dictionary<string, string[]> query, long ShopId);
    }
}