using Library.Dto.Employee;
using ShoeStore.Dto.Employee;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        Employee Create(Role role, EmployeeCreateDto dto);
        List<Employee> GetAll();
        Employee? Find(long id);
        Employee? Find(string login);
        bool CheckPassword(Employee employee, string password);
        bool Update(Role role, EmployeeInfoDto dto);
    }
}
