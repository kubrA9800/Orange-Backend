using Microsoft.AspNetCore.Mvc;

namespace Orange_Backend.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
