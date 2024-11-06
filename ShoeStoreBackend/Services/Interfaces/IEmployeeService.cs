using ShoeStore.Backend.Data;
using ShoeStore.Models;
using ShoeStoreBackend.Dto;

namespace ShoeStoreBackend.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Employee Create(Role role, EmployeeCreateDto dto);
        public Employee Find(long id);
        public Employee Find(string login);
        public bool CheckPassword(Employee employee, string password);
    }
}
