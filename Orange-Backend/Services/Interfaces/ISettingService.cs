using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetSettings();
        Task<List<Setting>> GetAllAsync();

    }
}
