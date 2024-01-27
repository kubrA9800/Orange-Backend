using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Configuration;
using NuGet.ContentModel;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels.Account;
using Orange_Backend.ViewModels.Cart;
using Orange_Backend.ViewModels.Wishlist;

namespace Orange_Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWishlistService _wishlistService;
        private readonly ICartService _cartService;
        private readonly AppDbContext _context;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IWishlistService wishlistService,
                                 ICartService cartService,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _wishlistService = wishlistService;
            _cartService = cartService;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser dbUser = await _userManager.FindByEmailAsync(request.Email);

            if (dbUser is null)
            {
                dbUser = await _userManager.FindByNameAsync(request.Email);
            }

            if (dbUser is null)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(dbUser, request.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View();

            }

            List<WishlistVM> wishlist = new();
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(dbUser.Id);

            List<CartVM> basket = new();
            Cart dbBasket = await _cartService.GetByUserIdAsync(dbUser.Id);

            if (dbBasket is not null)
            {
                List<CartProduct> basketProducts = await _cartService.GetAllByBasketIdAsync(dbBasket.Id);

                foreach (var item in basketProducts)
                {
                    basket.Add(new CartVM
                    {
                        ProductId = item.ProductId,
                        Count = item.Count
                    });
                }

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            }

            if (dbWishlist is not null)
            {
                List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbWishlist.Id);

                foreach (var item in wishlistProducts)
                {
                    wishlist.Add(new WishlistVM
                    {
                        ProductId = item.ProductId
                    });
                }

                Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

            }

            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }



            AppUser user = new()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();

            List<WishlistVM> wishlist = _wishlistService.GetDatasFromCookies();
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);

            List<CartVM> basket = _cartService.GetDatasFromCookies();
            Cart dbBasket = await _cartService.GetByUserIdAsync(userId);

            if (basket.Count != 0)
            {
                if (dbBasket == null)
                {
                    dbBasket = new()
                    {
                        AppUserId = userId,
                        CartProducts = new List<CartProduct>()
                    };

                    foreach (var item in basket)
                    {
                        dbBasket.CartProducts.Add(new CartProduct()
                        {
                            ProductId = item.ProductId,
                            CartId = dbBasket.Id,
                            Count = item.Count
                        });
                    }
                    await _context.Carts.AddAsync(dbBasket);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    List<CartProduct> basketProducts = new();

                    foreach (var item in basket)
                    {
                        basketProducts.Add(new CartProduct()
                        {
                            ProductId = item.ProductId,
                            CartId = dbBasket.Id,
                            Count = item.Count
                        });
                    }

                    dbBasket.CartProducts = basketProducts;
                    _context.SaveChanges();
                }

                Response.Cookies.Delete("basket");
            }
            else
            {
                if (dbBasket is not null)
                {
                    _context.Carts.Remove(dbBasket);
                    _context.SaveChanges();
                }

            }



            if (wishlist.Count != 0)
            {
                if (dbWishlist == null)
                {
                    dbWishlist = new()
                    {
                        AppUserId = userId,
                        WishlistProducts = new List<WishlistProduct>()

                    };

                    foreach (var item in wishlist)
                    {
                        dbWishlist.WishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = item.ProductId,
                            WishlistId = dbWishlist.Id
                        });

                    }

                    await _context.Wishlists.AddAsync(dbWishlist);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<WishlistProduct> wishlistProducts = new();

                    foreach (var item in wishlist)
                    {
                        wishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = item.ProductId,
                            WishlistId = dbWishlist.Id
                        });
                    }

                    dbWishlist.WishlistProducts = wishlistProducts;

                    _context.SaveChanges();

                }

                Response.Cookies.Delete("wishlist");
            }
            else
            {
                if (dbWishlist is not null)
                {
                    _context.Wishlists.Remove(dbWishlist);
                    _context.SaveChanges();
                }

            }

            return RedirectToAction("Index", "Home");
        }












        public IActionResult ForgetPassword()
        {
            return View();
        }

    }

}  

