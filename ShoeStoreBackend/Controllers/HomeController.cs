using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.Backend.Data;
using ShoeStoreBackend.Helpers;
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
        public IActionResult Login()
        {
            return new JsonResult(new Response("Hello, hacker"));
        }

        [Route("clients/list")]
        public IActionResult ClientsList()
        {
            return new JsonResult(_context.Clients);
        }
    }
}
