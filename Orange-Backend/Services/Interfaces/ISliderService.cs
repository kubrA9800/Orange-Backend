using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderVM> GetByIdAsync(int id);
		Task CreateAsync(SliderCreateVM slider);
        Task EditAsync(SliderEditVM slider);
        Task DeleteAsync(int id);




    }
}
