using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Areas.Admin.ViewModels.Magazine;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Areas.Admin.ViewModels.Subscribe;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;
using Orange_Backend.Models;

namespace Orange_Backend.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public InfoVM Infos { get; set; }
        public TreatmentVM Treatments { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public List<ProductVM> Products { get; set; }
        public List<MagazineVM> Magazines { get; set; }
        public List<BlogVM> Blogs { get; set; }
        public List<BrandVM> Brands { get; set; }
        public SubscribeCreateVM Subscribe { get; set; }
        public bool IsInWishlist { get; set; }
    }
}
