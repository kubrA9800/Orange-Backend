using Microsoft.AspNetCore.Mvc;

namespace Orange_Backend.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
