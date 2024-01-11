using Orange_Backend.Areas.Admin.ViewModels.Treatment;

namespace Orange_Backend.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<TreatmentVM> GetAllAsync();
    }
}
