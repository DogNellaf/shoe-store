using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.ItemIncome;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class ItemIncomeService: IItemIncomeService
    {
        private readonly ApplicationContext _context;
        private readonly IItemService _itemSerivce;

        public ItemIncomeService(ApplicationContext context, IItemService itemSerivce)
        {
            _context = context;
            _itemSerivce = itemSerivce;
        }

        public List<ItemIncome> CreateMany(List<ItemIncomeCreateDto> dtoList)
        {
            ArgumentNullException.ThrowIfNull(dtoList);

            var itemIncomes = new List<ItemIncome>();
            var createdAt = DateTime.Now;

            foreach (var dto in dtoList)
            {
                if (dto.ItemId != null && dto.Value != null)
                {
                    var value = dto.Value.Value;
                    var itemId = dto.ItemId.Value;

                    var item = _itemSerivce.Find(itemId);
                    if (item != null)
                    {
                        item.StorageCount += value;
                        var itemIncome = new ItemIncome()
                        {
                            Item = item,
                            Date = createdAt,
                            Value = value
                        };
                        itemIncomes.Add(itemIncome);
                    }
                }
            }

            _context.ItemIncomes.AddRange(itemIncomes);
            _context.SaveChanges();

            return itemIncomes;
        }
    }
}
