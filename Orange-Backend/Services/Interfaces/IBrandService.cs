using Orange_Backend.Areas.Admin.ViewModels.Brand;

namespace Orange_Backend.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
        
    }
}
