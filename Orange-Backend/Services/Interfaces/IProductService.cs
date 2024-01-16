using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task<ProductVM> GetByIdWithIncludesAsync(int id);
        Task<Product> GetProductDatasModalAsync(int id);


    }
}
