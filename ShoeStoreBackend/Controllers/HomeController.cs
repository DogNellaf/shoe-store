using Microsoft.AspNetCore.Mvc;
using ShoeStore.Backend.Data;

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

        [Route("")]
        public IActionResult Index()
        {
            return new JsonResult(_context.Clients.ToList());
        }
    }
}
