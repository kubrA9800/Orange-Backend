using Microsoft.AspNetCore.Mvc;

namespace Orange_Backend.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
