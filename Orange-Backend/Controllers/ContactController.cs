using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;

namespace Orange_Backend.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {
            ContactContent content= await _contactService.GetAllAsync();
            ContactPageVM model = new()
            {
               ContactContent = content
            };
            return View(model);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateMessage(ContactMessageCreateVM request)
        {

            await _contactService.CreateAsync(request);

            return RedirectToAction("Index", "Contact");

        }
    }
}
