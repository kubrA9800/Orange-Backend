﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public BlogController(IBlogService blogService,
                              AppDbContext context,
                              IWebHostEnvironment env,
                              IMapper mapper)
        {
            _blogService = blogService;
            _context = context;
            _env = env;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			BlogVM blog = await _blogService.GetByIdAsync((int)id);

			if (blog is null) return NotFound();

			return View(blog);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(BlogCreateVM request)
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

			await _blogService.CreateAsync(request);

			return RedirectToAction(nameof(Index));
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            BlogVM blog = await _blogService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            BlogEditVM blogEditVM = _mapper.Map<BlogEditVM>(blog);


            return View(blogEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id is null) return BadRequest();

            BlogVM dbBlog = await _blogService.GetByIdAsync((int)id);

            if (dbBlog is null) return NotFound();


            request.Image = dbBlog.Image;

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

            await _blogService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }

    }
}