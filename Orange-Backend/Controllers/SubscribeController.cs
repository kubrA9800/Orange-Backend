using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Subscribe;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;
        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubscribe(SubscribeCreateVM subscribe)
        {
            //if (!ModelState.IsValid) return RedirectToAction("Index", subscribe);
            //var data= _subscribeService.CreateAsync(subscribe);

            //if (data != null)
            //{
            //    ModelState.AddModelError("Email", "Email already exist");
            //    return RedirectToAction("Index","Home");
            //}
            await _subscribeService.CreateAsync(subscribe);
            return RedirectToAction("Index");
        }
    }
}
