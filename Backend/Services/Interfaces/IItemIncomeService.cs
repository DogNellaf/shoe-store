using ShoeStore.Dto.ItemIncome;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IItemIncomeService
    {
        List<ItemIncome> CreateMany(List<ItemIncomeCreateDto> dtoList);
    }
}
