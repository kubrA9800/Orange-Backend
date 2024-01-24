using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Models;

namespace Orange_Backend.Areas.Admin.ViewModels.Brand
{
    public class BrandVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<ProductVM> Products { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public List<string> CategoryName { get; set; }
        public List<BrandCategory> BrandCategories { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
