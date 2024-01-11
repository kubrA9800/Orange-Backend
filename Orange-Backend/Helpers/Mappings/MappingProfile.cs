using AutoMapper;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;
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

        }
    }
}
