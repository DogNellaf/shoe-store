using Library.Dto.Employee;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Employee;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ApplicationContext _context;
        private readonly byte[] _salt = [32, 60, 56, 148, 34, 118, 59, 74, 172, 22, 168, 15, 45, 79, 136, 230, 142, 88, 25, 31, 12, 115, 167, 31, 132, 5, 27, 95, 28, 17, 125, 235];
        public EmployeeService(ApplicationContext context)
        {
            _context = context;
        }

        public Employee Create(Role role, EmployeeCreateDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto.Login);
            ArgumentNullException.ThrowIfNull(dto.Password);

            var employee = new Employee()
            {
                Role = role,
                Login = dto.Login,
                Password = GetPasswordHash(dto.Password)
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.Include(x => x.Role).ToList();
        }

        public Employee? Find(long id)
        {
            return _context.Employees.Include(x => x.Role).SingleOrDefault(x => x.Id == id);
        }

        public Employee? Find(string login)
        {
            return _context.Employees.Include(x => x.Role).SingleOrDefault(x => x.Login == login);
        }

        public bool CheckPassword(Employee employee, string password)
        {
            ArgumentNullException.ThrowIfNull(employee);

            var passwordHash = GetPasswordHash(password);
            if (employee.Password == passwordHash)
            {
                return true;
            }
            return false;
        }

        protected string GetPasswordHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "";
            }

            byte[] bytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            );
            return Convert.ToBase64String(bytes);
        }

        public bool Update(Role role, EmployeeInfoDto dto)
        {
            var originalEmployee = Find(dto.Id);
            if (originalEmployee == null)
            {
                return false;
            }

            originalEmployee.Login = dto.Login;
            originalEmployee.Role = role;

            if (dto.Password != null)
            {
                originalEmployee.Password = GetPasswordHash(dto.Password);
            }

            _context.SaveChanges();

            return true;
        }
    }
}
