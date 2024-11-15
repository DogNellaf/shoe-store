using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Reservation;
using ShoeStore.Helpers;
using ShoeStore.Models;

namespace ShoeStore.Backend.Controllers
{
    [ApiController]
    [Route("api/reservations/")]
    public class ReservationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReservationService _service;
        private readonly IItemService _itemService;
        private readonly IClientSerivce _clientSerivce;
        public ReservationController(ILogger<HomeController> logger, IReservationService service, IItemService itemService, IClientSerivce clientSerivce)
        {
            _logger = logger;
            _service = service;
            _itemService = itemService;
            _clientSerivce = clientSerivce;
        }

        [Route("create")]
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] ReservationCreateDto? dto)
        {
            if (dto == null)
            {
                return new JsonResponse("Не были переданы данные", ResponseType.ValidationError);
            }

            if (dto.ItemId == null)
            {
                return new JsonResponse("Не был передан идентификатор товара", ResponseType.ValidationError);
            }

            Item? item = _itemService.Find(dto.ItemId.Value);
            if (item == null)
            {
                return new JsonResponse($"Товар с таким идентификатором не найден", ResponseType.ValidationError);
            }

            if (dto.Count == null)
            {
                return new JsonResponse("Не указано количество забронированных товаров", ResponseType.ValidationError);
            }

            if (dto.Count <= 0)
            {
                return new JsonResponse("Нельзя забронировать 0 или меньше товаров", ResponseType.ValidationError);
            }

            if (item.StorageCount < dto.Count)
            {
                return new JsonResponse("На складе недостаточно товара", ResponseType.ValidationError);
            }

            if (dto.FIO == null || dto.Phone == null)
            {
                return new JsonResponse("Не указаны данные клиента", ResponseType.ValidationError);
            }

            Client? client = _clientSerivce.Find(dto.FIO, dto.Phone.Value);
            if (client == null)
            {
                return new JsonResponse($"Клиент с такими данными не найден", ResponseType.ValidationError);
            }

            _service.Create(item, client, dto);

            return new JsonResponse("Бронирование успешно добавлено", ResponseType.Success);
        }

        [Route("cancel")]
        [HttpPost]
        [Authorize]
        public IActionResult Cancel([FromBody] ReservationCancelDto? dto)
        {
            if (dto == null)
            {
                return new JsonResponse("Не были переданы данные", ResponseType.ValidationError);
            }

            if (dto.ItemId == null)
            {
                return new JsonResponse("Не был передан идентификатор товара", ResponseType.ValidationError);
            }

            Item? item = _itemService.Find(dto.ItemId.Value);
            if (item == null)
            {
                return new JsonResponse($"Товар с таким идентификатором не найден", ResponseType.ValidationError);
            }

            if (dto.FIO == null || dto.Phone == null)
            {
                return new JsonResponse("Не указаны данные клиента", ResponseType.ValidationError);
            }

            Client? client = _clientSerivce.Find(dto.FIO, dto.Phone.Value);
            if (client == null)
            {
                return new JsonResponse($"Клиент с такими данными не найден", ResponseType.ValidationError);
            }

            bool isCanceled = _service.Cancel(item, client);

            if (isCanceled) {
                return new JsonResponse("Бронирование успешно отменено", ResponseType.Success);
            }

            return new JsonResponse("Бронирование не существует", ResponseType.Success);
        }
    }
}
