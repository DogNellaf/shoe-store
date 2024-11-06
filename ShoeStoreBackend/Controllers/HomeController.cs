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
    }
}
