using Microsoft.AspNetCore.Mvc.Rendering;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
		Task<BrandVM> GetByNameAsync(string name);
		Task<BrandVM> GetByIdAsync(int id);
		List<SelectListItem> GetAllSelectedAsync();
		Task CreateAsync(BrandCreateVM brand);
		Task EditAsync(BrandEditVM brand);
        Task DeleteAsync(int id);



    }
}
