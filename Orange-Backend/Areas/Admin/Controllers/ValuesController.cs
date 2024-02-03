using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Values;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    public class ValuesController : MainController
    {
        private readonly IValuesService _valueService;
        public ValuesController(IValuesService valuesService)
        {
            _valueService = valuesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _valueService.GetAllAsync());
        }


		[HttpGet]

		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			ValuesVM dbvalue = await _valueService.GetByIdAsync((int)id);

			if (dbvalue is null) return NotFound();

			return View(dbvalue);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			ValuesVM dbValue = await _valueService.GetByIdAsync((int)id);

			if (dbValue is null) return NotFound();

			return View(new ValuesEditVM
			{
				Id=dbValue.Id,
				Head = dbValue.Head,
				Text= dbValue.Text,
			});
		}



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, ValuesEditVM request)
        {
            if (id is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            ValuesVM dbValue = await _valueService.GetByIdAsync((int)id);

            if (dbValue is null) return NotFound();


           

          
            await _valueService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
