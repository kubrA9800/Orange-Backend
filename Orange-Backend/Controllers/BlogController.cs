using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> Index()
        {
            List<BlogVM> blogs = await _blogService.GetAllAsync();

            return View(blogs);
        }
        public async Task <IActionResult> Detail(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Index", "Error");
            }

            BlogVM existBlog = await _blogService.GetByIdAsync((int)id);

            if (existBlog == null)
            {
               return RedirectToAction("Index", "Error");
            }
            BlogVM blog = await _blogService.GetByIdAsync((int)id);

            return View(blog);
        }
    }
}
