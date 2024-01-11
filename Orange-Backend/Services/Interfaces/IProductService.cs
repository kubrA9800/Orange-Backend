using Orange_Backend.Areas.Admin.ViewModels.Product;

namespace Orange_Backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);

    }
}
