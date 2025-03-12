using ShoeStore.Dto.Item;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IItemService
    {
        Item Create(ItemCreateDto dto, Shop shop);
        Item? Find(long id);
        Item? Find(string title);
        void SetStorageCount(Item item, int storageCount);
        public List<Item> FindMany(string[] titles, string[] values, long shopId);
    }
}