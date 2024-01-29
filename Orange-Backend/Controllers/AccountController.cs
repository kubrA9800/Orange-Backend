using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Configuration;
using NuGet.ContentModel;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Enums;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IWishlistService wishlistService,
                                 ICartService cartService,
                                 AppDbContext context,
                                 RoleManager<IdentityRole> roleManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _wishlistService = wishlistService;
            _cartService = cartService;
            _context = context;
            _roleManager = roleManager;
            _emailService = emailService;
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


            var createdUser = await _userManager.FindByNameAsync(user.UserName);

            await _userManager.AddToRoleAsync(createdUser, Roles.Member.ToString());

            //qnhx hrab svdh vsgc
            //nxit ofmc bkco mghs



            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var url = Url.Action(nameof(VerifyEmail), "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());

            string subject = "Welcome to Orange";
            string emailHtml = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/register-confirm.html"))
            {
                emailHtml = reader.ReadToEnd();
            }

            emailHtml = emailHtml.Replace("{{link}}", url);
            emailHtml = emailHtml.Replace("{{fullName}}", user.FullName);

            _emailService.Send(user.Email, subject, emailHtml);


            return RedirectToAction("ConfirmEmail", "Account");
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser existUser = await _userManager.FindByEmailAsync(model.Email);

            if (existUser is null || !existUser.EmailConfirmed)
            {
                ModelState.AddModelError("Email", "User is not found");

                return View();
            }

            TempData["Email"] = existUser.Email;

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());
            string subject = "Reset Password";
            string html;

            using (StreamReader reader = new StreamReader("wwwroot/templates/reset-password.html"))
            {
                html = reader.ReadToEnd();
            }

            string fullName = existUser.FullName;

            html = html.Replace("{{fullName}}", fullName);
            html = html.Replace("{{link}}", link);

            _emailService.Send(existUser.Email, subject, html);

            return RedirectToAction(nameof(VerifyResetPassword));
        }


        [HttpGet]
        public IActionResult VerifyResetPassword()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { Token = token, UserId = userId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {

            if (!ModelState.IsValid) return View(resetPassword);
            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);
            if (existUser == null) return RedirectToAction("Index", "Error");

            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "New password can't be same as old password");
                return View(resetPassword);
            }
            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);
            return RedirectToAction(nameof(Login));

        }



        public IActionResult ConfirmEmail()
        {
            return View();
        }


        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId is null || token is null) return RedirectToAction("Index", "Error"); ;

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user is null) return RedirectToAction("Index", "Error"); ;

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }



        //Create Roles Method

        //[HttpGet]
        //public async Task<IActionResult> CreateRoles()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //    return Ok();
        //}

    }

}  

