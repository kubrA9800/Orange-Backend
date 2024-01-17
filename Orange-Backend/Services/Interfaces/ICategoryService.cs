using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int id);
    }
}
