using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;
        public ContactController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    IContactService contactService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _contactService = contactService;
        }


        [HttpGet]
        public async Task<IActionResult> ContentIndex()
        {
            return View(await _contactService.GetAllAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			ContactContent content = await _contactService.GetByIdAsync((int)id);

			if (content is null) return NotFound();

			return View(content);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			ContactContent content = await _context.ContactContents.FirstOrDefaultAsync(m => m.Id == id);

			if (content is null) return NotFound();

			ContactContentEditVM contentEdit = _mapper.Map<ContactContentEditVM>(content);


			return View(contentEdit);
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ContactContentEditVM request)
        {

            if (id is null) return BadRequest();

            ContactContent dbContent = await _context.ContactContents.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbContent is null) return NotFound();


            request.Image = dbContent.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is null)
            {
                _mapper.Map(request, dbContent);
                _context.ContactContents.Update(dbContent);
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


            await _contactService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> MessagesIndex()
        {
            return View(await _contactService.GetAllMessagesAsync());
        }


		[HttpGet]
		public async Task<IActionResult> MessageDetail(int? id)
		{
			if (id is null) return BadRequest();

			ContactMessage contactMessage = await _contactService.GetMessageByIdAsync((int)id);

			if (contactMessage is null) return NotFound();

			return View(contactMessage);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> MessageDelete(int id)
		{
			await _contactService.DeleteAsync(id);
			return RedirectToAction(nameof(MessagesIndex));
		}

	}
}
