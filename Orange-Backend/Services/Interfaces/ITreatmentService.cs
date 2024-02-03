using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;

namespace Orange_Backend.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<TreatmentVM> GetAllAsync();
        Task<TreatmentVM> GetByIdAsync(int id);
        Task EditAsync(TreatmentEditVM request);

    }
}
