using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();
            int productCount = await _productService.GetCountAsync();

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);
            ShopVM model = new()
            {
                Product = paginatedDatas,
                Categories = categories,
                Brands = brands,
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

        //[HttpGet]
        //public async Task<IActionResult> GetProductsByCategory(int[] selectedCategoryIds, int page = 1, int take = 6)
        //{
        //    if (selectedCategoryIds is null)
        //        return BadRequest();

        //    List<ProductVM> allProducts = new();

        //    foreach (var id in selectedCategoryIds)
        //    {
        //        var products = await _productService.GetProductsByCategoryAsync(id, page, take);
        //        allProducts.AddRange(products);
        //    }

        //    int pageCount = await GetPageCountByCategoryAsync(selectedCategoryIds, take);

        //    Paginate<ProductVM> model = new Paginate<ProductVM>(allProducts, page, pageCount);

        //    return PartialView("_ProductPartial", model);
        //}

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 6)
        {
            if (id is null)
            {
                return BadRequest();
            }

            CategoryVM existCategory = await _categoryService.GetByIdAsync((int)id);

            if (existCategory == null)
            {
                return NotFound();
            }

            var count = await _productService.GetCountByCategoryAsync((int)id);
            var allProductsCount = await _productService.GetCountAsync();

            List<ProductVM> dbPaginatedDatasByCategory = await _productService.GetPaginatedDatasByCategory((int)id, page, take);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();
          

            int pageCount = await GetPageCountByCategoryAsync((int) id, take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasByCategory, page, pageCount);

            ShopVM model = new()
            {
                CategoryId = (int)id,
                Categories = categories,
                Brands=brands,
                Product = paginatedDatas,
                ProductCount = count,
                AllProductsCount=allProductsCount,
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 6)
        {
            if (id is null)
            {
                return BadRequest();
            }

            BrandVM existBrand = await _brandService.GetByIdAsync((int)id);

            if (existBrand == null)
            {
                return NotFound();
            }

            int count = await _productService.GetCountByBrandAsync((int)id);
            int allProductsCount = await _productService.GetCountAsync();

            List<ProductVM> dbPaginatedDatasByCategory = await _productService.GetPaginatedDatasByBrand((int)id, page, take);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();


            int pageCount = await GetPageCountByBrandAsync((int)id, take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasByCategory, page, pageCount);

            ShopVM model = new()
            {
                BrandId = (int)id,
                Categories = categories,
                Brands = brands,
                Product = paginatedDatas,
                ProductCount = count,
                AllProductsCount = allProductsCount,
            };

            return View(model);
        }




        private async Task<int> GetPageCountByCategoryAsync(int id, int take)
        {
            int productCount = await _productService.GetCountByCategoryAsync(id);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }
        private async Task<int> GetPageCountByBrandAsync(int id, int take)
        {
            int productCount = await _productService.GetCountByBrandAsync(id);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }


        public async Task<IActionResult> Sort(string sortValue, int page = 1, int take = 6)
        {
            List<ProductVM> products = new();

            if (sortValue == "1")
            {
                return RedirectToAction("Index");

            };
           
            if (sortValue == "2")
            {
                products = await _productService.OrderByLatestDate(page, take);
            };

            if (sortValue == "3")
            {
                products = await _productService.OrderByPriceAsc(page, take);

                

            };
            if (sortValue == "4")
            {
                products = await _productService.OrderByPriceDesc(page, take);

            };


            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(products, page, pageCount);

            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();

            int count = await _productService.GetCountAsync();
            int allProductsCount = await _productService.GetCountAsync();


            ShopVM model = new()
            {
                Product = paginatedDatas,
                Categories = categories,
                Brands=brands,
                ProductCount = count,
                SortValue = sortValue,
                AllProductsCount = allProductsCount,
            };
            return View(model);
        }


        public async Task<IActionResult> Search(string searchText, int page = 1, int take = 6)
        {

            if (searchText == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            List<ProductVM> dbPaginatedDatasBySearch = await _productService.SearchAsync(searchText, page, take);

            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();

            int count = await _productService.GetCountBySearch(searchText);

            int pageCount = await GetPageCountBySearchAsync(searchText, take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasBySearch, page, pageCount);

            int allProductsCount = await _productService.GetCountAsync();



            ShopVM model = new()
            {
                Categories = categories,
                Brands = brands,
                Product = paginatedDatas,
                SearchText = searchText,
                ProductCount = count,
                AllProductsCount= allProductsCount

            };

            return View(model);
        }

        private async Task<int> GetPageCountBySearchAsync(string searchText, int take)
        {
            int productCount = await _productService.GetCountBySearch(searchText);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        private async Task<int> GetPageCountByFilterAsync(int value1, int value2, int take)
        {
            int productCount = await _productService.FilterCountAsync(value1, value2);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }


        [HttpGet]
        public async Task<IActionResult> Filter(int value1, int value2, int page = 1, int take = 6)
        {

            List<ProductVM> dbPaginatedDatasBySearch = await _productService.FilterAsync(value1, value2);

            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();

            int pageCount = await GetPageCountByFilterAsync(value1, value2, take);
            int count = await _productService.FilterCountAsync(value1,value2);


            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasBySearch, page, pageCount);

            ShopVM model = new()
            {
                Categories = categories,
                Brands = brands,
                Product = paginatedDatas,
                ProductCount = count,
                Value1 = value1,
                Value2 = value2

            };


            return View(model);

        }

        public async Task<IActionResult> FilterCount(int value1, int value2)
        {

            int filterCount = await _productService.FilterCountAsync(value1, value2);
            return Ok(filterCount);

        }


    }
}
