using Orange_Backend.Areas.Admin.ViewModels.Category;

namespace Orange_Backend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
    }
}
