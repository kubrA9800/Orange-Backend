using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();

    }
}
