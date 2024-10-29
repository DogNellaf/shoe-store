using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.Backend.Data;
using ShoeStore.Models;
using ShoeStoreBackend.Helpers;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ShoeStoreBackend.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("auth/login/{username}")]
        [HttpGet]
        public IActionResult Index(string username)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JsonResult(new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        [Route("")]
        [HttpGet]
        public IActionResult Login()
        {
            return new JsonResult(new Response("Hello, hacker"));
        }

        [Route("clients/list")]
        [HttpGet]
        public IActionResult ClientsList()
        {
            return new JsonResult(_context.Clients);
        }

        [Route("sales/create")]
        [HttpPost]
        public IActionResult SaleCreate()
        {
            var headers = Request.Headers;
            if (!headers.Keys.Contains("Token"))
            {
                return new JsonResult(new Response("Заголовки не содержат Token", 401, ResponseType.Error));
            }

            if (!headers.Keys.Contains("Content-Type"))
            {
                return new JsonResult(new Response("Заголовки не содержат Content-Type", 401, ResponseType.Error));
            }

            var contentType = headers["Content-Type"];
            if (contentType != "multipart/form-data" && contentType != "application/x-www-form-urlencoded")
            {
                return new JsonResult(new Response("Тип контента должен быть multipart/form-data или application/x-www-form-urlencoded", 401, ResponseType.Error));
            }

            if (!headers.Keys.Contains("Content-Type"))
            {
                return new JsonResult(new Response("Заголовки не содержат Content-Type", 401, ResponseType.Error));
            }

            string token = headers["Token"];
            // TODO: проверка авторизации

            var body = Request.Form;

            if (!body.Keys.Contains("item_id"))
            {
                return new JsonResult(new Response("Не указан item_id", 400, ResponseType.ValidationError));
            }

            //if (body["item_id"] == null)
            //{
            //    return new JsonResult(new Response("Поле item_id задано, но значение не указано", 400, ResponseType.ValidationError));
            //}

            if (body["item_id"].Count > 1)
            {
                return new JsonResult(new Response("В item_id получено не примитивное значение", 400, ResponseType.ValidationError));
            }

            var itemIdRaw = body["item_id"][0];

            if (!long.TryParse(itemIdRaw, out long itemId))
            {
                return new JsonResult(new Response("Поле item_id имеет некорректное значение", 400, ResponseType.ValidationError));
            }

            if (itemId < 0)
            {
                return new JsonResult(new Response("Поле item_id не может быть меньше 0", 400, ResponseType.ValidationError));
            }

            Item item = _context.Items.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
            {
                return new JsonResult(new Response($"Товар с таким id не найден", 400, ResponseType.ValidationError));
            }


            var isReturned = false;
            if (body.Keys.Contains("is_returned"))
            {
                try
                {
                    var isReturnedRaw = body["is_returned"][0];
                    if (isReturnedRaw != null)
                    {
                        isReturned = bool.Parse(isReturnedRaw);
                    }
                } 
                catch
                {
                    return new JsonResult(new Response("Указано некорректное значение поля is_returned", 400, ResponseType.ValidationError));
                }
            }

            var createdAt = DateTime.Now;


            return new JsonResult("Успешно");
        }

        [Route("items/create")]
        [HttpPost]
        public IActionResult ItemCreate()
        {
            var headers = Request.Headers;
            if (!headers.Keys.Contains("Token"))
            {
                return new JsonResult(new Response("Заголовки не содержат Token", 401, ResponseType.Error));
            }

            if (!headers.Keys.Contains("Content-Type"))
            {
                return new JsonResult(new Response("Заголовки не содержат Content-Type", 401, ResponseType.Error));
            }

            var contentType = headers["Content-Type"];
            if (contentType != "multipart/form-data" && contentType != "application/x-www-form-urlencoded")
            {
                return new JsonResult(new Response("Тип контента должен быть multipart/form-data или application/x-www-form-urlencoded", 401, ResponseType.Error));
            }

            if (!headers.Keys.Contains("Content-Type"))
            {
                return new JsonResult(new Response("Заголовки не содержат Content-Type", 401, ResponseType.Error));
            }

            string token = headers["Token"];
            // TODO: проверка авторизации

            var body = Request.Form;

            if (!body.Keys.Contains("shop_id"))
            {
                return new JsonResult(new Response("Не указан shop_id", 400, ResponseType.ValidationError));
            }

            //if (body["shop_id"] == null)
            //{
            //    return new JsonResult(new Response("Поле shop_id задано, но значение не указано", 400, ResponseType.ValidationError));
            //}

            if (body["shop_id"].Count > 1)
            {
                return new JsonResult(new Response("В shop_id получено не примитивное значение", 400, ResponseType.ValidationError));
            }

            var shopIdRaw = body["shop_id"][0];
            if (!long.TryParse(shopIdRaw, out long shopId))
            {
                return new JsonResult(new Response("Поле shop_id имеет некорректное значение", 400, ResponseType.ValidationError));
            }

            if (shopId < 0)
            {
                return new JsonResult(new Response("Поле shop_id не может быть меньше 0", 400, ResponseType.ValidationError));
            }

            Shop shop = _context.Shops.FirstOrDefault(x => x.Id == shopId);
            if (shop == null)
            {
                return new JsonResult(new Response($"Магазин с таким id не найден", 400, ResponseType.ValidationError));
            }

            if (!body.Keys.Contains("article"))
            {
                return new JsonResult(new Response("Не указан article", 400, ResponseType.ValidationError));
            }

            var article = body["article"][0];

            if (article == "")
            {
                return new JsonResult(new Response("Артикул не может быть пустым", 400, ResponseType.ValidationError));
            }

            if (!body.Keys.Contains("price"))
            {
                return new JsonResult(new Response("Не указан price", 400, ResponseType.ValidationError));
            }

            var priceRaw = body["price"][0];

            if (priceRaw == "" || priceRaw == null)
            {
                return new JsonResult(new Response("Поле price не задано", 400, ResponseType.ValidationError));
            }

            float price = float.Parse(priceRaw);

            if (!float.TryParse(priceRaw, out price))
            {
                return new JsonResult(new Response("Поле price имеет некорректное значение", 400, ResponseType.ValidationError));
            }

            if (price < 0)
            {
                return new JsonResult(new Response("Поле price не может быть меньше 0", 400, ResponseType.ValidationError));
            }

            var storageCountRaw = body["storage_count"][0];

            if (storageCountRaw == "" || storageCountRaw == null)
            {
                return new JsonResult(new Response("Поле storage_count не задано", 400, ResponseType.ValidationError));
            }
            
            if (!int.TryParse(storageCountRaw, out int storageCount))
            {
                return new JsonResult(new Response("Поле storage_count имеет некорректное значение", 400, ResponseType.ValidationError));
            }

            if (storageCount <= 0)
            {
                return new JsonResult(new Response("Поле storage_count не может быть меньше 0", 400, ResponseType.ValidationError));
            }

            var properitesRaw = body["property_ids"];
            var propertiesList = new List<Property>();

            for (int i = 0; i < properitesRaw.Count; i++)
            {
                var propertyIdRaw = properitesRaw[i];
                if (!int.TryParse(propertyIdRaw, out int propertyId))
                {
                    return new JsonResult(new Response($"Элемент {i} в списке характеристик имеет некорректное значение Id", 400, ResponseType.ValidationError));
                }

                var property = _context.Properties.FirstOrDefault(x => x.Id == propertyId);
                if (property == null)
                {
                    return new JsonResult(new Response($"Характеристика с id = {propertyId} не найдена", 400, ResponseType.ValidationError));
                }
                propertiesList.Add(property);
            }

            var item = new Item()
            {
                Id = -1,
                Shop = shop,
                Article = article,
                Price = price,
                StorageCount = storageCount,
                Properties = propertiesList
            };

            _context.Items.Add(item);

            return new JsonResult("Товар успешно добавлен");
        }
    }
}
