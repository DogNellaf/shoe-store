using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class ItemPropertiesService : IItemPropertiesService
    {
        private readonly ApplicationContext _context;
        public ItemPropertiesService(ApplicationContext context)
        {
            _context = context;
        }

        public List<ItemProperty> Create(Dictionary<string, string> properties, Item item)
        {
            var result = new List<ItemProperty>();

            if (properties != null)
            {
                foreach (var title in properties.Keys)
                {
                    var property = _context.Properties.FirstOrDefault(x => x.Title == title);
                    if (property == null)
                    {
                        property = new Property()
                        {
                            Title = title
                        };
                    }

                    var itemProperty = new ItemProperty()
                    {
                        Item = item,
                        Property = property,
                        Value = properties[title]
                    };
                    result.Add(itemProperty);
                }
            }

            _context.AddRange(result);
            _context.SaveChanges();
            return result;
        }
    }
}
