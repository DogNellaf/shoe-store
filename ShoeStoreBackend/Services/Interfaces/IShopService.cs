using ShoeStore.Dto.Shop;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IShopService
    {
        Shop Create(ShopCreateDto dto);
        Shop? Find(long id);
    }
}