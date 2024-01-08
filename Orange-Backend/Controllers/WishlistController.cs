using Microsoft.AspNetCore.Mvc;

namespace Orange_Backend.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
