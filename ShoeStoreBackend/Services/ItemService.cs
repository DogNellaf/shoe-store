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

        public List<Item> FindMany(Dictionary<string, string[]> query, long shopId)
        {
            var parameterTitles = query.Keys;
            var result = new List<Item>();

            var itemsProperties = _context.ItemsProperties.Include(x => x.Item.Shop.Id);
            var properties = _context.Properties;

            foreach (var title in parameterTitles)
            {
                var property = properties.FirstOrDefault(x => x.Title == title);
                if (property != null)
                {
                    var values = query[title];
                    var items = itemsProperties.Where(x => x.Property.Id == property.Id).Where(x => values.Contains(x.Value));
                    result.AddRange(items.Select(x => x.Item));
                }
            }

            return result.Where(x => x.Shop.Id == shopId).DistinctBy(x => x.Id).ToList();
        }
    }
}
