using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : MainController
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
				ModelState.AddModelError("Photo", "File can be only image format");
				return View(request);
			}

			if (!request.Photo.CheckFilesize(200))
			{
				ModelState.AddModelError("Photo", "File size can be max 200 kb");
				return View(request);
			}



			await _categoryService.CreateAsync(request);
			return RedirectToAction(nameof(Index));
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			Category dbCategory = await _context.Categories.AsNoTracking().Where(m => m.Id == id).Include(m => m.BrandCategories).FirstOrDefaultAsync();

			if (dbCategory is null) return NotFound();

			var selectedBrands = dbCategory.BrandCategories.Select(m => m.BrandId).ToList();

			var brands = _context.Brands.Select(m => new SelectListItem()
			{
				Text = m.Name,
				Value = m.Id.ToString(),
				Selected = selectedBrands.Contains(m.Id)
			}).ToList();


			return View(new CategoryEditVM()
			{
				Name = dbCategory.Name,
				Image=dbCategory.Image,
				Brands = brands,

			});

		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            

            Category dbCategory = await _context.Categories.Where(m => m.Id == id)
                                            .Include(m => m.BrandCategories)
                                            .ThenInclude(m => m.Brand)
                                            .FirstOrDefaultAsync();

            if (dbCategory is null) return NotFound();


            var selectedBrands = dbCategory.BrandCategories.Select(m => m.BrandId).ToList();

            request.Image = dbCategory.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);


            if (request.Photo != null)
            {
               
                
                    if (!request.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File can only be in image format");
                        return View(request);

                    }

                    if (!request.Photo.CheckFilesize(200))
                    {
                        ModelState.AddModelError("Photos", "File size can be max 200 kb");
                        return View(request);
                    }
                
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            


            if (existCategory is not null)
            {
                if (existCategory.Id == request.Id)
                {

                    await _categoryService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Name", "This category already exists");
                return View(request);
            }

            await _categoryService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _categoryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }


    }
}
