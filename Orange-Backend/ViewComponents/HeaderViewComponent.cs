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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public HeaderViewComponent(ILayoutService layoutService,
                                   IHttpContextAccessor httpContextAccessor,
                                   UserManager<AppUser> userManager)
        {
            _layoutService = layoutService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderVM model = await _layoutService.GetHeaderDatas();
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is not null)
            {
                AppUser currentUser = await _userManager.FindByIdAsync(userId);
                model.UserFullName = currentUser.FullName;
                model.UserId = currentUser.Id;
            }

            return await Task.FromResult(View(model));
        }
    }
}
