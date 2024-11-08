using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class RoleService: IRoleService
    {
        private readonly ApplicationContext _context;
        public RoleService(ApplicationContext context)
        {
            _context = context;
        }

        public Role Create(string title)
        {
            var role = new Role()
            {
                Title = title
            };
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Role? Find(long id)
        {
            return _context.Roles.SingleOrDefault(x => x.Id == id);
        }

        public Role? Find(string title)
        {
            return _context.Roles.SingleOrDefault(x => x.Title == title);
        }
    }
}
