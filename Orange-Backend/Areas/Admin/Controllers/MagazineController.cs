using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Magazine;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MagazineController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IMagazineService _magazineService;
        public MagazineController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    IMagazineService magazineService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _magazineService = magazineService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _magazineService.GetAllAsync());
        }


		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			MagazineVM magazine = await _magazineService.GetByIdAsync((int)id);

			if (magazine is null) return NotFound();

			return View(magazine);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(MagazineCreateVM request)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (!request.Photo.CheckFileType("image/"))
			{
				ModelState.AddModelError("Photo", "File can be only image format");
				return View();
			}

			if (!request.Photo.CheckFilesize(200))
			{
				ModelState.AddModelError("Photo", "File size can be max 200 kb");
				return View();
			}

			await _magazineService.CreateAsync(request);

			return RedirectToAction(nameof(Index));
		}

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            MagazineVM  magazine = await _magazineService.GetByIdAsync((int)id);

            if (magazine is null) return NotFound();

            MagazineEditVM magazineEdit = _mapper.Map<MagazineEditVM>(magazine);


            return View(magazineEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, MagazineEditVM request)
        {
            if (id is null) return BadRequest();

            MagazineVM dbMagazine = await _magazineService.GetByIdAsync((int)id);

            if (dbMagazine is null) return NotFound();


            request.Image = dbMagazine.Image;

            if (!ModelState.IsValid)
            {
                return View(request);

            }

            if (request.Photo is not null)
            {
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
            }

            await _magazineService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _magazineService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
