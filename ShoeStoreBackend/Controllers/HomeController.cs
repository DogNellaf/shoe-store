using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.Backend.Data;
using ShoeStore.Models;
using ShoeStoreBackend.Dto;
using ShoeStoreBackend.Helpers;
using ShoeStoreClassesLibrary;
using System.IdentityModel.Tokens.Jwt;
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

        protected IActionResult checkHeaders(IHeaderDictionary headers)
        {
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
            // TODO: сделать авторизацию

            return null;
        }
    }
}
