using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;
using System.Diagnostics;

namespace Orange_Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IInfoService _infoService;
        private readonly ITreatmentService _treatmentService;
        private readonly ICategoryService _categoryService;
        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                              IInfoService infoService,
                              ITreatmentService treatmentService,
                              ICategoryService categoryService)
        {
            _context = context;
            _sliderService = sliderService;
            _infoService = infoService;
            _treatmentService = treatmentService;
            _categoryService = categoryService;

        }
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            InfoVM infos= await _infoService.GetAllAsync();
            TreatmentVM treatments = await _treatmentService.GetAllAsync();
            List<CategoryVM> categories= await _categoryService.GetAllAsync();
            HomeVM model = new()
            {
                Sliders = sliders,
                Infos = infos,
                Treatments=treatments,
                Categories=categories
                

            };

            return View(model);
        }

       
    }
}
