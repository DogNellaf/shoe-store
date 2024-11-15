using Microsoft.EntityFrameworkCore;
using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Item;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
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
            ArgumentNullException.ThrowIfNull(dto.Article);
            ArgumentNullException.ThrowIfNull(dto.Price);
            ArgumentNullException.ThrowIfNull(dto.StorageCount);

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

        public Item? Find(long id)
        {
            return _context.Items.SingleOrDefault(x => x.Id == id);
        }

        public void SetStorageCount(Item item, int storageCount)
        {
            item.StorageCount = storageCount;
            _context.Update(item);
            _context.SaveChanges();
        }

        public List<Item> FindMany(string[] titles, string[] values, long shopId)
        {
            return  _context
                    .ItemsProperties
                    .Include(x => x.Property)
                    .Include(x => x.Item)
                    .Include(x => x.Item.Shop)
                    .Where(x => x.Item.Shop.Id == shopId)
                    .Where(x => x.Item.StorageCount > 0)
                    .Where(x => titles.Contains(x.Property.Title))
                    .Where(x => values.Contains(x.Value))
                    .Select(x => x.Item)
                    //.Distinct()
                    .ToList();
        }
    }
}
