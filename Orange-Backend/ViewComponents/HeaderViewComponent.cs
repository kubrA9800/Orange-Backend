using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;
using System.Security.Claims;

namespace Orange_Backend.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderVM model = _layoutService.GetHeaderDatas();

            return await Task.FromResult(View(model));
        }
    }
}
