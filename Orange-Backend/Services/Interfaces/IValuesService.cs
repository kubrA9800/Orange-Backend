using Orange_Backend.Areas.Admin.ViewModels.Values;

namespace Orange_Backend.Services.Interfaces
{
    public interface IValuesService
    {
        Task<ValuesVM> GetAllAsync();
        Task<ValuesVM> GetByIdAsync(int id);
        Task EditAsync(ValuesEditVM request);

    }
}
