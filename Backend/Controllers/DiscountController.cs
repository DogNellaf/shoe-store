using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Discount;
using ShoeStore.Helpers;

namespace ShoeStore.Backend.Controllers
{
    [ApiController]
    [Route("api/discounts/")]
    public class DiscountController : Controller
    {
        private const long MaxDiscountsDays = 365;
        private const double MaxDiscountMinutes = MaxDiscountsDays * 24 * 60;

        private readonly ILogger<HomeController> _logger;
        private readonly IDiscountService _service;

        public DiscountController(ILogger<HomeController> logger, IDiscountService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = "Admin|Merchandiser")]
        public IActionResult Create([FromBody] DiscountCreateDto dto)
        {
            if (dto.StartAt == null)
            {
                return new JsonResponse("Не указана дата начала скидки", ResponseType.ValidationError);
            }

            if (dto.StartAt.Value.Date < DateTime.UtcNow.Date)
            {
                return new JsonResponse("Скидка не может начинаться в прошлом", ResponseType.ValidationError);
            }

            if (dto.EndAt != null)
            {
                if (dto.EndAt.Value < dto.StartAt.Value)
                {
                    return new JsonResponse("Дата конца скидки не может быть раньше начала", ResponseType.ValidationError);
                }

                var ticks = dto.EndAt.Value.Ticks - dto.StartAt.Value.Ticks;
                var duration = new TimeSpan(ticks).TotalMinutes;

                if (duration > MaxDiscountMinutes)
                {
                    return new JsonResponse($"Скидка не может длиться больше {MaxDiscountsDays} дней", ResponseType.ValidationError);
                }
            }

            if (dto.Value == null)
            {
                return new JsonResponse("Не указано значение скидки в % (от 0.01 до 99.99)", ResponseType.ValidationError);
            }

            if (dto.Value < 0.01)
            {
                return new JsonResponse("Скидка не может быть меньше 0.01", ResponseType.ValidationError);
            }

            if (dto.Value > 99.99)
            {
                return new JsonResponse("Скидка не может быть больше 99.99", ResponseType.ValidationError);
            }

            _service.Create(dto);

            return new JsonResponse("Скидка успешно добавлена", ResponseType.Success);
        }

        [Route("attach")]
        [HttpPost]
        [Authorize(Roles = "Admin,Merchandiser")]
        public IActionResult Attach([FromBody] DiscountAttachDto dto)
        {
            if (dto.DiscountId == null)
            {
                return new JsonResponse("Не указан id скидки", ResponseType.ValidationError);
            }

            if (dto.DiscountId <= 0)
            {
                return new JsonResponse("Id скидки не может быть меньше или равно 0", ResponseType.ValidationError);
            }

            var discount = _service.Find(dto.DiscountId.Value);
            if (discount == null)
            {
                return new JsonResponse("Скидка с таким id не найдена", ResponseType.ValidationError);
            }

            if (discount.EndAt != null && discount.EndAt > DateTime.Now)
            {
                return new JsonResponse("Нельзя прикрепить товары к завершившейся скидке", ResponseType.ValidationError);
            }

            if (dto.ItemIds == null)
            {
                return new JsonResponse("Не указаны id товаров", ResponseType.ValidationError);
            }

            if (dto.ItemIds.Length == 0)
            {
                return new JsonResponse("Не указаны id товаров", ResponseType.ValidationError);
            }

            _service.AttachDisconutItems(discount, dto.ItemIds);

            return new JsonResponse("Скидка успешно прикреплена к указанным товарам", ResponseType.Success);
        }
    }
}
