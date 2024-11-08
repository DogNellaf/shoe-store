using ShoeStore.Models;
using ShoeStoreClassesLibrary;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IItemPropertiesService
    {
        List<ItemProperty> Create(Dictionary<string, string> properties, Item item);
    }
}