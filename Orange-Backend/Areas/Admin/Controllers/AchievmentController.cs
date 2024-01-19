using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AchievmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IAchievmentService _achievmentService;
        public AchievmentController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    IAchievmentService achievmentService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _achievmentService = achievmentService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _achievmentService.GetAllAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			AchievmentVM achievment = await _achievmentService.GetByIdAsync((int)id);

			if (achievment is null) return NotFound();

			return View(achievment);
		}


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Achievment achievment = await _context.Achievments.FirstOrDefaultAsync(m => m.Id == id);

            if (achievment is null) return NotFound();

            AchievmentEditVM achievmentEditVM = _mapper.Map<AchievmentEditVM>(achievment);


            return View(achievmentEditVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AchievmentEditVM request)
        {

            if (id is null) return BadRequest();

            Achievment dbAchievment = await _context.Achievments.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbAchievment is null) return NotFound();


            request.Image = dbAchievment.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is null)
            {
                _mapper.Map(request, dbAchievment);
                _context.Achievments.Update(dbAchievment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }

                if (!request.Photo.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 200 kb");
                    return View(request);
                }
            }


            await _achievmentService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
