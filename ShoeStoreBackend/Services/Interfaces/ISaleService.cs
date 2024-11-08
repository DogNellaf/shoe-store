using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface ISaleService
    {
        Sale Create(Item item, Employee employee, bool isReturned, DateTime createdAt);
        List<Sale> CreateMany(Item item, Employee employee, bool isReturned, DateTime createdAt, int count);
    }
}