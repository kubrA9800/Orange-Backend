using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Areas.Admin.ViewModels.Magazine;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Areas.Admin.ViewModels.Subscribe;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services;
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
        private readonly IProductService _productService;
        private readonly IMagazineService _magazineService;
        private readonly IBlogService _blogService;
        private readonly IBrandService _brandService;
        private readonly ISubscribeService _subscribeService;
        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                              IInfoService infoService,
                              ITreatmentService treatmentService,
                              ICategoryService categoryService,
                              IProductService productService,
                              IMagazineService magazineService,
                              IBlogService blogService,
                              IBrandService brandService,
                              ISubscribeService subscribeService)
        {
            _context = context;
            _sliderService = sliderService;
            _infoService = infoService;
            _treatmentService = treatmentService;
            _categoryService = categoryService;
            _productService= productService;
            _magazineService= magazineService;
            _blogService= blogService; 
            _brandService= brandService;
            _subscribeService= subscribeService;

        }
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            InfoVM infos= await _infoService.GetAllAsync();
            TreatmentVM treatments = await _treatmentService.GetAllAsync();
            List<ProductVM> products = await _productService.GetAllAsync();

            List<CategoryVM> categories= await _categoryService.GetAllAsync();
            List<MagazineVM> magazines = await _magazineService.GetAllAsync();
            List<BlogVM> blogs = await _blogService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();
            

            HomeVM model = new()
            {
                Sliders = sliders,
                Infos = infos,
                Treatments=treatments,
                Categories=categories,
                Products = products,
                Magazines=magazines,
                Blogs=blogs,
                Brands=brands
                

            };

            return View(model);
        }

        public async Task<IActionResult> GetProductDatasModal(int? id)
        {
            if (id is null) return BadRequest();
            var dbProduct = await _productService.GetProductDatasModalAsync((int)id);
            if (dbProduct is null) return NotFound();
            var categoryName=dbProduct.Category.Name;
            var mainImage = dbProduct.Images.Where(p => p.IsMain).FirstOrDefault().Image;

            ModalVM model = new()
            {
                Id = dbProduct.Id,
                Name = dbProduct.Name,
                Price = dbProduct.Price,
                Description = dbProduct.Description,
                CategoryName = categoryName,
                Image = mainImage

            };
            return Ok(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubscribe(SubscribeCreateVM subscribe)
        {

            await _subscribeService.CreateAsync(subscribe);
            return RedirectToAction("Index", "Subscribe");
        }


    }
}
