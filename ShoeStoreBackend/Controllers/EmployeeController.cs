using Microsoft.AspNetCore.Mvc;
using ShoeStoreBackend.Dto;
using ShoeStoreBackend.Helpers;
using ShoeStoreBackend.Services;
using ShoeStoreBackend.Services.Interfaces;

namespace ShoeStoreBackend.Controllers
{
    [ApiController]
    [Route("api/employees/")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;

        public EmployeeController(ILogger<HomeController> logger, IEmployeeService service, IRoleService roleService)
        {
            _logger = logger;
            _employeeService = service;
            _roleService = roleService;
        }


        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] EmployeeCreateDto dto)
        {
            if (dto.RoleId == null)
            {
                return new JsonResponse("Не указан id роли", ResponseType.ValidationError);
            }

            var role = _roleService.Find(dto.RoleId.Value);
            if (role == null)
            {
                return new JsonResponse("Роль с таким id не существует", ResponseType.ValidationError);
            }

            if (string.IsNullOrEmpty(dto.Login))
            {
                return new JsonResponse("Не указан логин", ResponseType.ValidationError);
            }

            var sameEmployee = _employeeService.Find(dto.Login);
            if (sameEmployee != null)
            {
                return new JsonResponse("Сотрудник с таким логином уже существует", ResponseType.ValidationError);
            }

            if (string.IsNullOrEmpty(dto.Password))
            {
                return new JsonResponse("Не указан пароль", ResponseType.ValidationError);
            }

            _employeeService.Create(role, dto);
            return new JsonResponse("Сотрудник успешно добавлен", ResponseType.Success);
        }
    }
}