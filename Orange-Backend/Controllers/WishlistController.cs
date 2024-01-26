using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels.Wishlist;

namespace Orange_Backend.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _wishlistService.GetWishlistDatasAsync());
        }




        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _wishlistService.DeleteItem(id);
            List<WishlistVM> wishlist = _wishlistService.GetDatasFromCookies();

            return Ok(wishlist.Count);
        }

      
    }
}
