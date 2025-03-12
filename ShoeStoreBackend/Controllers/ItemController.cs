using Library.Dto.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Item;
using ShoeStore.Helpers;
using ShoeStore.Models;

namespace ShoeStore.Backend.Controllers
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
        [Authorize(Roles = "Admin,Merchandiser")]
        public IActionResult Create([FromBody] ItemCreateDto? dto)
        {
            if (dto == null)
            {
                return new JsonResponse("Не были переданы данные", ResponseType.ValidationError);
            }

            if (dto.Properties == null)
            {
                return new JsonResponse("Не были переданы параметры товара", ResponseType.ValidationError);
            }

            if (dto.ShopId == null)
            {
                return new JsonResponse("Не указан id магазина", ResponseType.ValidationError);
            }

            if (dto.ShopId < 0)
            {
                return new JsonResponse("Id магазина не может быть меньше 0", ResponseType.ValidationError);
            }

            Shop? shop = _shopService.Find(dto.ShopId.Value);
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

        [Route("search")]
        [HttpPost]
        [Authorize]
        public IActionResult Search([FromBody] ItemsSearchDto dto)
        {
            if (dto.ShopId == null)
            {
                return new JsonResponse("Не указан id магазина", ResponseType.ValidationError);
            }

            if (dto.Parameters == null)
            {
                return new JsonResponse("Не указаны параметры поиска", ResponseType.ValidationError);
            }

            var titles = dto.Parameters.Keys.ToArray();
            var values = dto.Parameters.SelectMany(x => x.Value).ToArray();

            return new JsonResult(
                _itemService
                .FindMany(titles, values, dto.ShopId.Value)
                .Select(x => new ItemInfoDto(x))
            );
        }

        [Route("{title}")]
        [HttpGet]
        public IActionResult Get(string title)
        {
            var item = _itemService.Find(title);
            if (item == null)
            {
                return new JsonResponse("Товар с таким названием не найден", ResponseType.Error);
            }

            var result = new Dictionary<string, object>()
            {
                {
                    "result", new ItemInfoDto(item)
                }
            };

            return new JsonResponse(result, ResponseType.Info);
        }
    }
}
