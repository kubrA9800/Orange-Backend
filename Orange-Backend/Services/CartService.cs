using Newtonsoft.Json;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Responses;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels.Cart;

namespace Orange_Backend.Services
{
    public class CartService:ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public CartService(IHttpContextAccessor httpContextAccessor,
                           AppDbContext context,
                           IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _productService = productService;

        }
        public List<CartVM> GetDatasFromCookie()
        {
            List<CartVM> carts;
            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                carts = new List<CartVM>();
            }
            return carts;
        }

        public void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct)
        {
            if (existProduct == null)
            {
                carts.Add(new CartVM
                {
                    ProductId = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(carts));
        }

        public void AddToBasket(int id, Product product)
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

            CartVM existProducts = basket.FirstOrDefault(m => m.ProductId == product.Id);

            if (existProducts is null)
            {
                basket.Add(new CartVM { ProductId = product.Id, Count = 1 });
            }
            else
            {
                existProducts.Count++;

            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public int GetCount()
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
            return basket.Sum(m => m.Count);

        }

        public async Task<List<CartDetailVM>> GetBasketDatasAsync()
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
                    Image = existProduct.Images.FirstOrDefault().Image
                });
            }
            return basketDetailList;
        }


        public async Task<DeleteBasketItemResponse> DeleteItem(int id)
        {
            List<decimal> grandTotal = new();

            List<CartVM> basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

            CartVM basketItem = basket.FirstOrDefault(m => m.ProductId == id);

            basket.Remove(basketItem);

            foreach (var item in basket)
            {
                var product = await _productService.GetByIdWithIncludesAsync(item.ProductId);


                decimal total = item.Count * product.Price;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return new DeleteBasketItemResponse
            {
                Count = basket.Sum(m => m.Count),
                GrandTotal = grandTotal.Sum()
            };
        }


        public async Task<CountResponse> IncreaseProductCount(int id)
        {
            List<decimal> grandTotal = new();

            List<CartVM> basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

            CartVM existProduct = basket.FirstOrDefault(m => m.ProductId == id);

            existProduct.Count++;

            var basketItem = await _productService.GetByIdWithIncludesAsync(id);

            var productTotalPrice = existProduct.Count * basketItem.Price;

            foreach (var item in basket)
            {

                var product = await _productService.GetByIdWithIncludesAsync(item.ProductId);

                decimal total = item.Count * product.Price;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return new CountResponse
            {
                CountItem = existProduct.Count,
                GrandTotal = grandTotal.Sum(),
                ProductTotalPrice = productTotalPrice,
                CountBasket = basket.Sum(m => m.Count)

            };
        }


        public async Task<CountResponse> DecreaseProductCount(int id)
        {
            List<decimal> grandTotal = new();

            List<CartVM> basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            CartVM existProduct = basket.FirstOrDefault(m => m.ProductId == id);


            if (existProduct.Count > 1)
            {

                existProduct.Count--;


            }
            foreach (var item in basket)
            {

                var product = await _productService.GetByIdWithIncludesAsync(item.ProductId);

                decimal total = item.Count * product.Price;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            var basketItem = await _productService.GetByIdWithIncludesAsync(id);
            var productTotalPrice = existProduct.Count * basketItem.Price;
            return new CountResponse
            {
                CountItem = existProduct.Count,
                GrandTotal = grandTotal.Sum(),
                ProductTotalPrice = productTotalPrice,
                CountBasket = basket.Sum(m => m.Count)
            };
        }

    }
}
