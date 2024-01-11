using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;

namespace Orange_Backend.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM model = _layoutService.GetFooterDatas();
            return await Task.FromResult(View(model));
        }
    }
}
