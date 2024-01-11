using Orange_Backend.Areas.Admin.ViewModels.Info;

namespace Orange_Backend.Services.Interfaces
{
    public interface IInfoService
    {
        Task<InfoVM> GetAllAsync();
    }
}
