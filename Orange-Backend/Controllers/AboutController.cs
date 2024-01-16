using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Result;
using Orange_Backend.Areas.Admin.ViewModels.Values;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;

namespace Orange_Backend.Controllers
{
    public class AboutController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IValuesService _valuesService;
        private readonly IAchievmentService _achievmentService;
        private readonly IResultService _resultService;
        public AboutController(IBannerService bannerService,
                               IValuesService valuesService,
                               IAchievmentService achievmentService,
                               IResultService resultService)
        {
            _bannerService = bannerService;
            _valuesService = valuesService;
            _achievmentService = achievmentService;
            _resultService = resultService;
        }
        public async Task<IActionResult> Index()
        {
            BannerVM banner=await _bannerService.GetAllAsync();
            ValuesVM value= await _valuesService.GetAllAsync();
            AchievmentVM achievment =await _achievmentService.GetAllAsync();
            ResultVM result= await _resultService.GetAllAsync();

            AboutVM model = new()
            { 
                Banner = banner,
                Value = value,
                Achievment=achievment,
                Result=result

            };
            return View(model);
        }
    }
}
