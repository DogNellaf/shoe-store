using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class SaleService : ISaleService
    {
        private readonly ApplicationContext _context;
        public SaleService(ApplicationContext context)
        {
            _context = context;
        }

        public Sale Create(Item item, Employee employee, bool isReturned, DateTime createdAt)
        {
            var sale = new Sale()
            {
                CreatedAt = createdAt,
                Employee = employee,
                Item = item,
                IsReturned = isReturned
            };
            _context.Sales.Add(sale);
            _context.SaveChanges();
            return sale;
        }

        public List<Sale> CreateMany(Item item, Employee employee, bool isReturned, DateTime createdAt, int count)
        {
            var sales = new List<Sale>();
            for (int i = 0; i < count; i++)
            {
                var sale = new Sale()
                {
                    CreatedAt = createdAt,
                    Employee = employee,
                    Item = item,
                    IsReturned = isReturned
                };
                sales.Add(sale);
            }
            _context.Sales.AddRange(sales);
            _context.SaveChanges();
            return sales;
        }
    }
}
