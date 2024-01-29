using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Result;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResultController : MainController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IResultService _resultService;
        public ResultController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    IResultService resultService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _resultService = resultService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _resultService.GetAllAsync());
        }


		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			ResultVM result = await _resultService.GetByIdAsync((int)id);

			if (result is null) return NotFound();

			return View(result);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			Result result = await _context.Results.FirstOrDefaultAsync(m => m.Id == id);

			if (result is null) return NotFound();

			ResultEditVM resultEdit = _mapper.Map<ResultEditVM>(result);


			return View(resultEdit);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ResultEditVM request)
        {

            if (id is null) return BadRequest();

            Result dbresult = await _context.Results.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbresult is null) return NotFound();


            request.Image = dbresult.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is null)
            {
                _mapper.Map(request, dbresult);
                _context.Results.Update(dbresult);
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


            await _resultService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
