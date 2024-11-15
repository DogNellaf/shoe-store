using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IClientSerivce
    {
        Client? Find(long id);
        Client Create(string FIO, long phone);
        Client? Find(string FIO, long phone);
    }
}
