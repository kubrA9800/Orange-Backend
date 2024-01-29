using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : MainController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        public BrandController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    ICategoryService categoryService,
                                    IBrandService brandService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _categoryService = categoryService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _brandService.GetAllAsync();


			return View(data);
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			BrandVM brand = await _brandService.GetByIdAsync((int)id);

			if (brand is null) return NotFound();

			return View(brand);
		}

		public async Task<IActionResult> Create()
		{

			var categories = _categoryService.GetAllSelectedAsync();

			BrandCreateVM model = new BrandCreateVM
			{
				Categories = categories
			};

			return View(model);
		}



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BrandCreateVM request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            BrandVM existBrand = await _brandService.GetByNameAsync(request.Name);

            if (existBrand is not null)
            {
                ModelState.AddModelError("Name", "This brand already exists");

                return View(request);
            }


            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File can be only image format");
                return View(request);
            }

            if (!request.Photo.CheckFilesize(200))
            {
                ModelState.AddModelError("Photo", "File size can be max 200 kb");
                return View(request);
            }



            await _brandService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Brand dbBrand = await _context.Brands.AsNoTracking().Where(m => m.Id == id).Include(m => m.BrandCategories).FirstOrDefaultAsync();

            if (dbBrand is null) return NotFound();

            var selectedCategories = dbBrand.BrandCategories.Select(m => m.CategoryId).ToList();

            var categories = _context.Categories.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
                Selected = selectedCategories.Contains(m.Id)
            }).ToList();


            return View(new BrandEditVM()
            {
                Name = dbBrand.Name,
                Image = dbBrand.Image,
                Categories = categories,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, BrandEditVM request)
        {
            if (id is null) return BadRequest();



            Brand dbBrand = await _context.Brands.Where(m => m.Id == id)
                                            .Include(m => m.BrandCategories)
                                            .ThenInclude(m => m.Category)
                                            .FirstOrDefaultAsync();

            if (dbBrand is null) return NotFound();


            var selectedBrands = dbBrand.BrandCategories.Select(m => m.CategoryId).ToList();

            request.Image = dbBrand.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            BrandVM existBrand = await _brandService.GetByNameAsync(request.Name);


            if (request.Photo != null)
            {


                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can only be in image format");
                    return View(request);

                }

                if (!request.Photo.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(request);
                }



            }
           


            if (existBrand is not null)
            {
                if (existBrand.Id == request.Id)
                {

                    await _brandService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Name", "This brand already exists");
                return View(request);
            }

            await _brandService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _brandService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }



    }
}
