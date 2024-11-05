using ShoeStore.Models;
using ShoeStoreBackend.Dto;

namespace ShoeStoreBackend.Services.Interfaces
{
    public interface IItemService
    {
        Item Create(ItemCreateDto dto, Shop shop);
        Item Find(long id);
        public void SetStorageCount(Item item, int storageCount);
    }
}