using AutoMapper;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Areas.Admin.ViewModels.Magazine;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Areas.Admin.ViewModels.Result;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;
using Orange_Backend.Areas.Admin.ViewModels.Values;
using Orange_Backend.Models;

namespace Orange_Backend.Helpers.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderVM>();
            CreateMap<Info, InfoVM>();
            CreateMap<Treatment, TreatmentVM>();
            CreateMap<Category, CategoryVM>();
            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Magazine, MagazineVM>();
            CreateMap<Blog, BlogVM>();
            CreateMap<Brand, BrandVM>();
            CreateMap<Banner, BannerVM>();
            CreateMap<Values, ValuesVM>();
            CreateMap<Achievment, AchievmentVM>();
            CreateMap<Result, ResultVM>();
           
            CreateMap<ContactMessageCreateVM, ContactMessage>().ReverseMap();





        }
    }
}
