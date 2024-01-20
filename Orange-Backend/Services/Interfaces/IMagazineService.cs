using Orange_Backend.Areas.Admin.ViewModels.Magazine;

namespace Orange_Backend.Services.Interfaces
{
    public interface IMagazineService
    {
        Task<List<MagazineVM>> GetAllAsync();
        Task<MagazineVM> GetByIdAsync(int id);
        Task CreateAsync(MagazineCreateVM request);
        Task EditAsync(MagazineEditVM request);
        Task DeleteAsync(int id);

    }
}
