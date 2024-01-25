using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;

namespace Orange_Backend.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CartController(ICartService cartService,
                              IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;

        }
        public async Task<IActionResult> Index()
        {
            List<ProductVM> products = await _productService.GetAllAsync();
            var cartDatas = await _cartService.GetBasketDatasAsync();

            CartPageVM model = new()
            {
                Products = products,
                CartDetails = cartDatas
            };
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var data = await _cartService.DeleteItem(id);

            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> IncreaseProductCount(int id)
        {
            var data = await _cartService.IncreaseProductCount(id);
            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> DecreaseProductCount(int id)
        {
            var data = await _cartService.DecreaseProductCount(id);
            return Ok(data);
        }
    }
}
