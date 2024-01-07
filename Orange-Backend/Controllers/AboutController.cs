using Microsoft.AspNetCore.Mvc;

namespace Orange_Backend.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
