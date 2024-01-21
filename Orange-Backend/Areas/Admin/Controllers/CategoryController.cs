using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        public CategoryController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    ICategoryService categoryService,
                                    IBrandService brandService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _categoryService= categoryService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			CategoryVM category = await _categoryService.GetByIdAsync((int)id);

			if (category is null) return NotFound();

			return View(category);
		}

		public async Task<IActionResult> Create()
		{

			var brands =  _brandService.GetAllSelectedAsync();

			CategoryCreateVM model = new CategoryCreateVM
			{
				Brands = brands
			};

			return View(model);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(CategoryCreateVM request)
		{

			if (!ModelState.IsValid)
			{
				return View(request);
			}

			CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);

			if (existCategory is not null)
			{
				ModelState.AddModelError("Name", "This category already exists");

				return View(request);
			}


			if (!request.Photo.CheckFileType("image/"))
			{
				ModelState.AddModelError("Photos", "File can be only image format");
				return View(request);
			}

			if (!request.Photo.CheckFilesize(200))
			{
				ModelState.AddModelError("Photos", "File size can be max 200 kb");
				return View(request);
			}



			await _categoryService.CreateAsync(request);
			return RedirectToAction(nameof(Index));
		}

	}
}
