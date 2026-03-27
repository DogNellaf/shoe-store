using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Sale;
using ShoeStore.Helpers;
using ShoeStore.Models;

namespace ShoeStore.Backend.Controllers
{
    [ApiController]
    [Route("api/sales/")]
    public class SaleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISaleService _saleService;
        private readonly IItemService _itemSerivce;
        private readonly IEmployeeService _employeeService;

        public SaleController(ILogger<HomeController> logger, ISaleService saleService, IItemService itemSerivce, IEmployeeService employeeService)
        {
            _logger = logger;
            _saleService = saleService;
            _itemSerivce = itemSerivce;
            _employeeService = employeeService;
        }

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = "Seller")]
        public IActionResult Create([FromBody] SaleCreateDto dto)
        {

            if (dto.ItemId == null)
            {
                return new JsonResponse("Не указан id товара", ResponseType.ValidationError);
            }

            if (dto.Count == null)
            {
                return new JsonResponse("Не указано количество товаров", ResponseType.ValidationError);
            }

            if (dto.Count <= 0)
            {
                return new JsonResponse($"Количество не может быть меньше или равно 0", ResponseType.ValidationError);
            }

            Item? item = _itemSerivce.Find(dto.ItemId.Value);
            if (item == null)
            {
                return new JsonResponse($"Товар с таким id не найден", ResponseType.ValidationError);
            }

            if (item.StorageCount - dto.Count < 0 && !dto.IsReturned)
            {
                return new JsonResponse($"Товара с id {item.Id} на складе недостаточно", ResponseType.ValidationError);
            }

            if (dto.EmployeeId == null)
            {
                return new JsonResponse("Не указан id сотрудника", ResponseType.ValidationError);
            }

            Employee? employee = _employeeService.Find(dto.EmployeeId.Value);
            if (employee == null)
            {
                return new JsonResponse($"Сотрудник с таким id не найден", ResponseType.ValidationError);
            }

            var createdAt = DateTime.Now;

            _saleService.CreateMany(item, employee, dto.IsReturned, createdAt, dto.Count.Value);

            int coef = 1;
            if (dto.IsReturned)
            {
                coef = -1;
            }

            int newStorageCount = item.StorageCount - dto.Count.Value * coef;
            _itemSerivce.SetStorageCount(item, newStorageCount);

            var message = "Продажа успешно добавлена";
            if (dto.Count > 1)
            {
                message = "Продажи успешно добавлены";
            }
            return new JsonResponse(message, ResponseType.Success);
        }
    }
}
