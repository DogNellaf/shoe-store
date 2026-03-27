using Library.Dto.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Employee;
using ShoeStore.Helpers;

namespace ShoeStore.Backend.Controllers
{
    [ApiController]
    [Route("api/employees/")]
    [Authorize(Roles = "Admin")]
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
            if (string.IsNullOrEmpty(dto.Role))
            {
                return new JsonResponse("Не указана роль", ResponseType.ValidationError);
            }

            var role = _roleService.Find(dto.Role);
            if (role == null)
            {
                return new JsonResponse("Роль с таким названием не существует", ResponseType.ValidationError, 404);
            }

            if (string.IsNullOrEmpty(dto.Login))
            {
                return new JsonResponse("Не указан логин", ResponseType.ValidationError);
            }

            var sameEmployee = _employeeService.Find(dto.Login);
            if (sameEmployee != null)
            {
                return new JsonResponse("Сотрудник с таким логином уже существует", ResponseType.ValidationError, 409);
            }

            if (string.IsNullOrEmpty(dto.Password))
            {
                return new JsonResponse("Не указан пароль", ResponseType.ValidationError);
            }

            _employeeService.Create(role, dto);
            return new JsonResponse("Сотрудник успешно добавлен", ResponseType.Success);
        }

        [Route("update")]
        [HttpPut]
        public IActionResult Update([FromBody] EmployeeInfoDto dto)
        {
            if (string.IsNullOrEmpty(dto.Role))
            {
                return new JsonResponse("Не указана роль", ResponseType.ValidationError);
            }

            var role = _roleService.Find(dto.Role);
            if (role == null)
            {
                return new JsonResponse("Роль с таким названием не существует", ResponseType.ValidationError, 404);
            }

            if (string.IsNullOrEmpty(dto.Login))
            {
                return new JsonResponse("Не указан логин", ResponseType.ValidationError);
            }

            var sameEmployee = _employeeService.Find(dto.Id);
            if (sameEmployee == null)
            {
                return new JsonResponse("Сотрудник с таким Id не существует", ResponseType.ValidationError, 409);
            }

            var isCompleted = _employeeService.Update(role, dto);
            if (isCompleted)
            {
                return new JsonResponse("Сотрудник успешно изменен", ResponseType.Success);
            }
            else
            {
                return new JsonResponse("Не удалось изменить пользователя", ResponseType.Error);
            }
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new Dictionary<string, object>()
            {
                {
                    "result", _employeeService.GetAll().Select(x => new EmployeeInfoDto(x.Id, x.Login, x.Role))
                }
            };

            return new JsonResponse(result, ResponseType.Info);
        }

        [Route("{login}")]
        [HttpGet]
        public IActionResult Get(string login)
        {
            var employee = _employeeService.Find(login);
            if (employee == null)
            {
                return new JsonResponse("Сотрудник с таким именем не найден", ResponseType.Error);
            }

            var employeeDto = new EmployeeInfoDto(employee.Id, employee.Login, employee.Role);

            var result = new Dictionary<string, object>()
            {
                {
                    "result", employeeDto
                }
            };

            return new JsonResponse(result, ResponseType.Info);
        }
    }
}