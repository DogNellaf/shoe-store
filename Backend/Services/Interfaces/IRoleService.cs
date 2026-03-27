using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IRoleService
    {
        public Role Create(string title);
        public Role? Find(long id);
        public Role? Find(string title);
    }
}
