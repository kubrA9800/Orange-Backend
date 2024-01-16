using Orange_Backend.Areas.Admin.ViewModels.Result;

namespace Orange_Backend.Services.Interfaces
{
    public interface IResultService
    {
        Task<ResultVM> GetAllAsync();
    }
}
