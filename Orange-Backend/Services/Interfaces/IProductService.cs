using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task<Product> GetByIdWithIncludesAsync(int id);
        Task<Product> GetProductDatasModalAsync(int id);
        Task<List<ProductVM>> GetProductsByCategoryAsync(int id, int page, int take);
        Task<List<ProductVM>> GetProductsByBrandAsync(int id, int page, int take);
        Task DeleteAsync(int id);

        Task<int> GetCountByCategoryAsync(int id);
        Task<int> GetCountByBrandAsync(int id);

        Task<List<ProductVM>> OrderByPriceAsc(int page, int take);
        Task<List<ProductVM>> OrderByPriceDesc(int page, int take);
        Task<List<ProductVM>> OrderByLatestDate(int page, int take);
        Task<List<ProductVM>> SearchAsync(string searchText, int page, int take);
        Task<int> GetCountBySearch(string searchText);
        Task<List<ProductVM>> FilterAsync(int value1, int value2);
        Task<int> FilterCountAsync(int value1, int value2);
        Task<List<ProductVM>> GetPaginatedDatasByCategory(int id, int page, int take);
        Task<List<ProductVM>> GetPaginatedDatasByBrand(int id, int page, int take);
    }
}
