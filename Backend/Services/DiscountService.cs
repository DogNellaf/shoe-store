using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Discount;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly ApplicationContext _context;
        private readonly IItemService _itemService;

        public DiscountService(ApplicationContext context, IItemService itemService)
        {
            _context = context;
            _itemService = itemService;
        }

        public Discount Create(DiscountCreateDto dto)
        {
            if (dto.StartAt == null) {
                throw new ArgumentNullException("dto.StartAt");
            }

            if (dto.EndAt == null) {
                throw new ArgumentNullException("dto.EndAt");
            }

            if (dto.Value == null) {
                throw new ArgumentNullException("dto.Value");
            }

            var discount = new Discount()
            {
                StartAt = dto.StartAt.Value,
                EndAt = dto.EndAt.Value,
                Value = dto.Value.Value
            };
            _context.Discounts.Add(discount);
            _context.SaveChanges();
            return discount;
        }

        public Discount? Find(long id)
        {
            return _context.Discounts.SingleOrDefault(x => x.Id == id);
        }

        public bool Contains(long id)
        {
            return _context.Discounts.Where(x => x.Id == id).Count() != 0;
        }

        public void AttachDisconutItems(Discount discount, long[] itemIds)
        {
            var itemsDiscounts = new List<ItemDiscount>();
            foreach (long id in itemIds)
            {
                var item = _itemService.Find(id);
                if (item != null)
                {
                    var itemDiscount = new ItemDiscount()
                    {
                        Item = item,
                        Discount = discount
                    };
                    itemsDiscounts.Add(itemDiscount);
                }
            }
            _context.ItemsDiscounts.AddRange(itemsDiscounts);
        }
    }
}
