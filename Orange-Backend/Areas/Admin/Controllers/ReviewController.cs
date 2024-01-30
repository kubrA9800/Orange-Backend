using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Review;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : MainController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _reviewService.GetAllAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			ReviewVM review = await _reviewService.GetByIdAsync((int)id);

			if (review is null) return NotFound();

			return View(review);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			await _reviewService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
