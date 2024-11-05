using ShoeStore.Backend.Data;
using ShoeStore.Models;
using ShoeStoreBackend.Dto;
using ShoeStoreBackend.Services.Interfaces;
using static System.Formats.Asn1.AsnWriter;

namespace ShoeStoreBackend.Services
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

        public Shop Find(long id)
        {
            return _context.Shops.FirstOrDefault(x => x.Id == id);
        }
    }
}
