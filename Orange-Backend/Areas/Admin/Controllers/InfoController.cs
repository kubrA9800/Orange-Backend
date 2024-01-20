using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class InfoController : Controller
	{

		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;
		private readonly IInfoService _infoService;
		public InfoController(AppDbContext context,
									IWebHostEnvironment env,
									IMapper mapper,
									IInfoService infoService)
		{
			_context = context;
			_env = env;
			_mapper = mapper;
			_infoService = infoService;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _infoService.GetAllAsync());
		}


		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			InfoVM info = await _infoService.GetByIdAsync((int)id);

			if (info is null) return NotFound();

			return View(info);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			Info info = await _context.Infos.FirstOrDefaultAsync(m => m.Id == id);

			if (info is null) return NotFound();

			InfoEditVM infoEdit = _mapper.Map<InfoEditVM>(info);


			return View(infoEdit);
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, InfoEditVM request)
        {

            if (id is null) return BadRequest();

            Info dbInfo = await _context.Infos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbInfo is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _infoService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
