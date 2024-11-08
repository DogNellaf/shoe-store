using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;

namespace ShoeStore.Backend.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Discount> Discounts { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<ItemIncome> ItemIncomes { get; set; } = null!;
        public DbSet<ItemProperty> ItemsProperties { get; set; } = null!;
        public DbSet<ItemDiscount> ItemsDiscounts { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<Shop> Shops { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
