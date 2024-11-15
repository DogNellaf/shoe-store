using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class ClientService: IClientSerivce
    {
        private readonly ApplicationContext _context;
        public ClientService(ApplicationContext context)
        {
            _context = context;
        }

        public Client? Find(long id)
        {
            return _context.Clients.SingleOrDefault(x => x.Id == id);
        }

        public Client? Find(string FIO, long phone)
        {
            return _context.Clients.SingleOrDefault(x => x.FIO == FIO && x.Phone == phone);
        }

        public Client Create(string FIO, long phone)
        {
            var client = new Client()
            {
                FIO = FIO,
                Phone = phone
            };
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }
    }
}
