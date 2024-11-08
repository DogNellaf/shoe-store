using ShoeStore.Dto.Employee;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Employee Create(Role role, EmployeeCreateDto dto);
        public Employee? Find(long id);
        public Employee? Find(string login);
        public bool CheckPassword(Employee employee, string password);
    }
}
