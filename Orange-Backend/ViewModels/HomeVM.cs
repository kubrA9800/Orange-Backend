using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
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
    }
}
