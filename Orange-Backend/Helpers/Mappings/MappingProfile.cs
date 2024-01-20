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
using Orange_Backend.Areas.Admin.ViewModels.Setting;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Areas.Admin.ViewModels.Subscribe;
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
            CreateMap<SliderEditVM, SliderVM>().ReverseMap();
            CreateMap<SliderCreateVM, Slider>();
            CreateMap<Info, InfoVM>();
            CreateMap<Info, InfoEditVM>().ReverseMap();
            CreateMap<Treatment, TreatmentVM>();
            CreateMap<Category, CategoryVM>();
            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Magazine, MagazineVM>();
            CreateMap<MagazineCreateVM, Magazine>();
            CreateMap<MagazineEditVM, MagazineVM>().ReverseMap();
            CreateMap<MagazineEditVM, Magazine>();
            CreateMap<Blog, BlogVM>();
            CreateMap<BlogCreateVM, Blog>();
            CreateMap<BlogEditVM, BlogVM>().ReverseMap();
            CreateMap<BlogEditVM, Blog>();
            CreateMap<Brand, BrandVM>();
            CreateMap<Setting, SettingEditVM>().ReverseMap();
            CreateMap<Banner, BannerVM>();
            CreateMap<Banner, BannerEditVM>().ReverseMap();

            CreateMap<Values, ValuesVM>();
            CreateMap<Values, ValuesEditVM>().ReverseMap();
            CreateMap<Achievment, AchievmentVM>();
            CreateMap<Achievment, AchievmentEditVM>().ReverseMap();
            CreateMap<Result, ResultVM>();
            CreateMap<ContactContent, ContactContentEditVM>().ReverseMap();
            CreateMap<Result, ResultEditVM>().ReverseMap();
            CreateMap<SubscribeCreateVM, Subscribe>().ReverseMap();
            CreateMap<Subscribe, SubscribeVM>();

            CreateMap<ContactMessageCreateVM, ContactMessage>().ReverseMap();





        }
    }
}
