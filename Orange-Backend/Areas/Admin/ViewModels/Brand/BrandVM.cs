using Orange_Backend.Areas.Admin.ViewModels.Product;

namespace Orange_Backend.Areas.Admin.ViewModels.Brand
{
    public class BrandVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<ProductVM> Products { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
