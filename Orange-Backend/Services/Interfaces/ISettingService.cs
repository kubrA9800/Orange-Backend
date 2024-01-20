using Orange_Backend.Areas.Admin.ViewModels.Setting;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetSettings();
        Task<List<Setting>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
        Task EditAsync(SettingEditVM request);
       


    }
}
