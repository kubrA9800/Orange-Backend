using Orange_Backend.Areas.Admin.ViewModels.Subscribe;

namespace Orange_Backend.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task CreateAsync(SubscribeCreateVM subscribe);
        Task<List<SubscribeVM>> GetAllAsync();
        Task DeleteAsync(int id);

    }
}
