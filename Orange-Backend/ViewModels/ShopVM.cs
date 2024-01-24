using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Helpers;
using Orange_Backend.Models;

namespace Orange_Backend.ViewModels
{
    public class ShopVM
    {
        public List<CategoryVM> Categories { get; set; }
        public List<BrandVM> Brands { get; set; }
        public Paginate<ProductVM> Product { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ProductCount { get; set; }
        public string SearchText { get; set; }
        public string SortValue { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int AllProductsCount { get; set; }

    }
}
