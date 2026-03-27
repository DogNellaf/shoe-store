using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Shop;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class ShopService : IShopService
    {
        private readonly ApplicationContext _context;
        public ShopService(ApplicationContext context)
        {
            _context = context;
        }

        public Shop Create(ShopCreateDto dto)
        {
            var shop = new Shop()
            {
                Title = dto.Title,
                Address = dto.Address
            };
            _context.Shops.Add(shop);
            _context.SaveChanges();
            return shop;
        }

        public Shop? Find(long id)
        {
            return _context.Shops.SingleOrDefault(x => x.Id == id);
        }
    }
}
