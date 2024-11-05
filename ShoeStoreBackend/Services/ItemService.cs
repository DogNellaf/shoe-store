using Microsoft.Identity.Client;
using ShoeStore.Backend.Data;
using ShoeStore.Models;
using ShoeStoreBackend.Dto;
using ShoeStoreBackend.Services.Interfaces;

namespace ShoeStoreBackend.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationContext _context;
        public ItemService(ApplicationContext context)
        {
            _context = context;
        }

        public Item Create(ItemCreateDto dto, Shop shop)
        {
            var item = new Item()
            {
                Shop = shop,
                Article = dto.Article,
                Price = dto.Price.Value,
                StorageCount = dto.StorageCount.Value
            };

            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Item Find(long id)
        {
            return _context.Items.FirstOrDefault(x => x.Id == id);
        }

        public void SetStorageCount(Item item, int storageCount)
        {
            item.StorageCount = storageCount;
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
