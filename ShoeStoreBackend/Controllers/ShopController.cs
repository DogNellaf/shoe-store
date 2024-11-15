using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Shop;
using ShoeStore.Helpers;

namespace ShoeStore.Backend.Controllers
{
    [ApiController]
    [Route("api/shops/")]
    [Authorize(Roles = "Admin")]
    public class ShopController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShopService _service;

        public ShopController(ILogger<HomeController> logger, IShopService service)
        {
            _logger = logger;
            _service = service;
        }


        [Route("create")]
        [HttpPost]
        public IActionResult ShopCreate([FromBody] ShopCreateDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
            {
                return new JsonResponse("Не указано название", ResponseType.ValidationError);
            }

            if (string.IsNullOrEmpty(dto.Address))
            {
                return new JsonResponse("Не указан адрес", ResponseType.ValidationError);
            }

            _service.Create(dto);

            return new JsonResponse("Магазин успешно добавлен", ResponseType.Success);
        }
    }
}
