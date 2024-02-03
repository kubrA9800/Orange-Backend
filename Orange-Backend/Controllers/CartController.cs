using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;
using Orange_Backend.ViewModels.Cart;

namespace Orange_Backend.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartController(ICartService cartService,
                              IProductService productService,
                              IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;

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

        public async Task<IActionResult> GetSidebarProducts()
        {
            List<CartVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<CartVM>();

            }

            List<CartDetailVM> basketDetailList = new();
            foreach (var item in basket)
            {
                Product existProduct = await _productService.GetByIdWithIncludesAsync(item.ProductId);

                basketDetailList.Add(new CartDetailVM
                {
                    Id = existProduct.Id,
                    Name = existProduct.Name,
                    Price = existProduct.Price,
                    Count = item.Count,
                    Total = existProduct.Price * item.Count,
                    Image = existProduct.Images.FirstOrDefault().Image,
                    

                    
                });
            }
            return Ok(basketDetailList);
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
