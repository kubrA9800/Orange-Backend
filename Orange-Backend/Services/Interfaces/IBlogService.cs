using Orange_Backend.Areas.Admin.ViewModels.Blog;

namespace Orange_Backend.Services.Interfaces
{
    public interface IBlogService
    {
       Task<List<BlogVM>> GetAllAsync();
        Task<BlogVM> GetByIdAsync(int id);
		Task CreateAsync(BlogCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(BlogEditVM request);



    }
}
