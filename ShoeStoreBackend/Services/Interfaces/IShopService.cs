using ShoeStore.Models;
using ShoeStoreBackend.Dto;

namespace ShoeStoreBackend.Services.Interfaces
{
    public interface IShopService
    {
        Shop Create(ShopCreateDto dto);
        Shop Find(long id);
    }
}