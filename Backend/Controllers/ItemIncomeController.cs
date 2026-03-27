using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.ItemIncome;
using ShoeStore.Helpers;

namespace ShoeStore.Backend.Controllers
{
    [ApiController]
    [Route("api/incomes/")]
    public class ItemIncomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemIncomeService _service;

        public ItemIncomeController(ILogger<HomeController> logger, IItemIncomeService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("create")]
        [HttpPost]
        [Authorize]
        public IActionResult CreateMany([FromBody] List<ItemIncomeCreateDto>? dtoList)
        {
            if (dtoList == null)
            {
                return new JsonResponse("Не указаны товары и количества", ResponseType.ValidationError);
            }

            if (dtoList.Count == 0)
            {
                return new JsonResponse("Не указаны товары и количества", ResponseType.ValidationError);
            }

            _service.CreateMany(dtoList);

            return new JsonResponse("Поступления успешно добавлены", ResponseType.Success);
        }
    }
}
