using Orange_Backend.Areas.Admin.ViewModels.Blog;

namespace Orange_Backend.Services.Interfaces
{
    public interface IBlogService
    {
       Task<List<BlogVM>> GetAllAsync();
        Task<BlogVM> GetByIdAsync(int id);



    }
}
