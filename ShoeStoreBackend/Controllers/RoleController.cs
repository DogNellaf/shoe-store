using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Role;
using ShoeStore.Helpers;

namespace ShoeStore.Backend.Controllers
{
    [ApiController]
    [Route("api/roles/")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleService _service;

        public RoleController(ILogger<HomeController> logger, IRoleService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] RoleCreateDto dto)
        {

            if (string.IsNullOrEmpty(dto.Title))
            {
                return new JsonResponse("Не указано название роли", ResponseType.ValidationError);
            }

            var sameRole = _service.Find(dto.Title);
            if (sameRole != null)
            {
                return new JsonResponse($"Роль с таким названием уже существует", ResponseType.ValidationError);
            }

            _service.Create(dto.Title);

            return new JsonResponse("Роль успешно добавлена", ResponseType.Success);
        }
    }
}
