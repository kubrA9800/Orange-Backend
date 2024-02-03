using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    public class TreatmentController : MainController
    {
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;
		private readonly ITreatmentService _treatmentService;
        public TreatmentController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    ITreatmentService treatmentService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _treatmentService = treatmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _treatmentService.GetAllAsync());
        }


		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			TreatmentVM treatment = await _treatmentService.GetByIdAsync((int)id);

			if (treatment is null) return NotFound();

			return View(treatment);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			Treatment treatment = await _context.Treatments.FirstOrDefaultAsync(m => m.Id == id);

			if (treatment is null) return NotFound();

			TreatmentEditVM treatmentEditVM = _mapper.Map<TreatmentEditVM>(treatment);


			return View(treatmentEditVM);
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TreatmentEditVM request)
        {

            if (id is null) return BadRequest();

            Treatment dbTreatment = await _context.Treatments.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbTreatment is null) return NotFound();


            request.Image1 = dbTreatment.Image1;
            request.Image2 = dbTreatment.Image2;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo1 is null || request.Photo2 is null)
            {
                _mapper.Map(request, dbTreatment);
                _context.Treatments.Update(dbTreatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!request.Photo1.CheckFileType("image/")&&request.Photo1.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }

                if (!request.Photo1.CheckFilesize(200)&& !request.Photo2.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 200 kb");
                    return View(request);
                }
            }


            await _treatmentService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
