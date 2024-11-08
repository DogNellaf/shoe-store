using ShoeStore.Dto.ItemIncome;
using ShoeStoreClassesLibrary;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IItemIncomeService
    {
        List<ItemIncome> CreateMany(List<ItemIncomeCreateDto> dtoList);
    }
}
