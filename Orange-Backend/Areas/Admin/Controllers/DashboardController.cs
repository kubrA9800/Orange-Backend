using Microsoft.AspNetCore.Mvc;

namespace Orange_Backend.Areas.Admin.Controllers
{
    public class DashboardController : MainController
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
