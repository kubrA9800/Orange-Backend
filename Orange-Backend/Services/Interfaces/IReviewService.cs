using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Areas.Admin.ViewModels.Review;

namespace Orange_Backend.Services.Interfaces
{
    public interface IReviewService
    {
         Task<List<ReviewVM>> GetAllAsync();
        Task<ReviewVM> GetByIdAsync(int id);
		Task DeleteAsync(int id);
        Task<List<ReviewVM>> GetReviewsByProductAsync(int id);


	}
}
