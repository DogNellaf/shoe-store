using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoeStore.Backend.Controllers
{

    [Route("api/auth/")]
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _service;

        public AuthController(ILogger<HomeController> logger, IEmployeeService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("login/{username}")]
        [HttpGet]
        public IActionResult Login(string username, [FromQuery] string password)
        {
            var user = _service.Find(username);

            if (user == null)
            {
                return new JsonResponse("Пользователь не найден", ResponseType.Error);
            }

            if (_service.CheckPassword(user, password) == false)
            {
                return new JsonResponse("Указаны неверные данные авторизации", ResponseType.Error);
            }

            var claims = new List<Claim> {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Role, user.Role.Title)
            };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonResponse(
                new Dictionary<string, object>()
                {
                    {"token", token },
                    {"role", user.Role.Title }
                },
                ResponseType.Info
            );
        }

        [Route("refresh/{username}")]
        [HttpGet]
        public IActionResult Refresh(string username, [FromQuery] string password)
        {
            return Login(username, password);
        }
    }
}
