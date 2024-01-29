using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribesController : MainController
    {
        private readonly ISubscribeService _subscribeService;
        public SubscribesController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _subscribeService.GetAllAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscribeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
