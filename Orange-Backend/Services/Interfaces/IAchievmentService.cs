using Orange_Backend.Areas.Admin.ViewModels.Achievment;

namespace Orange_Backend.Services.Interfaces
{
    public interface IAchievmentService
    {
         Task<AchievmentVM> GetAllAsync(); 
    }
}
