using Orange_Backend.Areas.Admin.ViewModels.Magazine;

namespace Orange_Backend.Services.Interfaces
{
    public interface IMagazineService
    {
        Task<List<MagazineVM>> GetAllAsync();
    }
}
