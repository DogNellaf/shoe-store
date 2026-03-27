using ShoeStore.Dto.Discount;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IDiscountService
    {
        Discount Create(DiscountCreateDto dto);
        bool Contains(long id);
        Discount? Find(long id);
        void AttachDisconutItems(Discount discount, long[] itemIds);
    }
}
