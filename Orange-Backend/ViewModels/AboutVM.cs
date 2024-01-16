using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Result;
using Orange_Backend.Areas.Admin.ViewModels.Values;

namespace Orange_Backend.ViewModels
{
    public class AboutVM
    {
        public BannerVM Banner { get; set; }
        public ValuesVM Value { get; set; }
        public AchievmentVM Achievment { get; set; }
        public ResultVM Result { get; set; }
    }
}
