using Microsoft.AspNetCore.Mvc;
using ShoeStore.Models;
using ShoeStoreBackend.Dto;
using ShoeStoreBackend.Helpers;
using ShoeStoreBackend.Services.Interfaces;

namespace ShoeStoreBackend.Controllers
{
    [ApiController]
    [Route("api/items/")]
    public class ItemController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itemService;
        private readonly IShopService _shopService;
        private readonly IItemPropertiesService _propertiesService;

        public ItemController(ILogger<HomeController> logger, IItemService service, IShopService shopService, IItemPropertiesService propertiesService)
        {
            _logger = logger;
            _itemService = service;
            _shopService = shopService;
            _propertiesService = propertiesService;
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] ItemCreateDto dto)
        {

            if (dto.ShopId == null)
            {
                return new JsonResponse("Не указан id магазина", ResponseType.ValidationError);
            }

            if (dto.ShopId < 0)
            {
                return new JsonResponse("Id магазина не может быть меньше 0", ResponseType.ValidationError);
            }

            Shop shop = _shopService.Find(dto.ShopId.Value);
            if (shop == null)
            {
                return new JsonResponse($"Магазин с таким id не найден", ResponseType.ValidationError);
            }

            if (string.IsNullOrEmpty(dto.Article))
            {
                return new JsonResponse("Не указан артикул", ResponseType.ValidationError);
            }

            if (dto.Price == null)
            {
                return new JsonResponse($"Не указана цена", ResponseType.ValidationError);
            }

            if (dto.Price < 0)
            {
                return new JsonResponse("Цена не может быть меньше 0", ResponseType.ValidationError);
            }

            if (dto.StorageCount == null)
            {
                return new JsonResponse($"Не указано количество на складе", ResponseType.ValidationError);
            }

            if (dto.StorageCount <= 0)
            {
                return new JsonResponse("Количество на складе не может быть меньше или равно 0", ResponseType.ValidationError);
            }

            var item = _itemService.Create(dto, shop);

            _propertiesService.Create(dto.Properties, item);

            return new JsonResponse("Товар успешно добавлен", ResponseType.Success);
        }
    }
}
