using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;

namespace Orange_Backend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingService _settingService;
        public LayoutService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public HeaderVM GetHeaderDatas()
        {

            Dictionary<string, string> settingDatas = _settingService.GetSettings();
            //int basketCount = _cartService.GetCount();

            return new HeaderVM
            {
                //BasketCount = basketCount,
                Logo = settingDatas["Logo"],

            };
        }
        public FooterVM GetFooterDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            return new FooterVM
            {
                Phone = settingDatas["Phone"],
                Email = settingDatas["Email"],
            };
        }
    }
}
