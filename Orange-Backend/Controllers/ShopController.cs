﻿using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Helpers;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

    }
}
