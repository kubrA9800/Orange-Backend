using Orange_Backend.Areas.Admin.ViewModels.Banner;

namespace Orange_Backend.Services.Interfaces
{
    public interface IBannerService
    {
        Task<BannerVM> GetAllAsync();
    }
}
