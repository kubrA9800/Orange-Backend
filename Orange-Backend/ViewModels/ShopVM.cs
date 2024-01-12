using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Helpers;

namespace Orange_Backend.ViewModels
{
    public class ShopVM
    {
        public List<CategoryVM> Category { get; set; }
        public List<BrandVM> Brand { get; set; }
        public Paginate<ProductVM> Product { get; set; }
        public int ProductCount { get; set; }

    }
}
