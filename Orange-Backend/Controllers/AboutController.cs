using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Values;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;

namespace Orange_Backend.Controllers
{
    public class AboutController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IValuesService _valuesService;
        public AboutController(IBannerService bannerService,
                               IValuesService valuesService)
        {
            _bannerService = bannerService;
            _valuesService = valuesService;
        }
        public async Task<IActionResult> Index()
        {
            BannerVM banner=await _bannerService.GetAllAsync();
            ValuesVM value= await _valuesService.GetAllAsync();

            AboutVM model = new()
            { 
                Banner = banner,
                Value = value

            };
            return View(model);
        }
    }
}
