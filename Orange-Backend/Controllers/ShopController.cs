using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Helpers;
using Orange_Backend.Models;
using Orange_Backend.Services;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;

namespace Orange_Backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        public ShopController(IProductService productService, 
                              ICategoryService categoryService,
                              IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 6)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);
            List<CategoryVM> category = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();
            int productCount = await _productService.GetCountAsync();

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);
            ShopVM model = new()
            {
                Product = paginatedDatas,
                Category = category,
                Brand = brands,
                ProductCount = productCount
            };
            return View(model);
        }

        public async Task<IActionResult> GetProducts(int page = 1, int take = 6)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);
            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return PartialView("_ProductPartial", paginatedDatas);
        }

       

        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            Product existProduct = await _productService.GetByIdWithIncludesAsync((int)id);

            if (existProduct == null)
            {
                return NotFound();
            }

            Product product = await _productService.GetByIdWithIncludesAsync((int)id);


            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int[] selectedCategoryIds, int page = 1, int take = 6)
        {
            if (selectedCategoryIds is null)
                return BadRequest();

            List<ProductVM> allProducts = new();

            foreach (var id in selectedCategoryIds)
            {
                var products = await _productService.GetProductsByCategoryAsync(id, page, take);
                allProducts.AddRange(products);
            }

            int pageCount = await GetPageCountByCategoryAsync(selectedCategoryIds, take);

            Paginate<ProductVM> model = new Paginate<ProductVM>(allProducts, page, pageCount);

            return PartialView("_ProductPartial", model);
        }

       


        private async Task<int> GetPageCountByCategoryAsync(int[] id, int take)
        {
            int productCount = await _productService.GetCountByCategoryAsync(id);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

    }
}
