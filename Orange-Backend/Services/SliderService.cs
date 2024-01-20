using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Slider;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SliderService(AppDbContext context,
                              IMapper mapper,
                              IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<SliderVM>> GetAllAsync()
        {
            return _mapper.Map<List<SliderVM>>(await _context.Sliders.ToListAsync());

        }

        public async Task<SliderVM> GetByIdAsync(int id)
        {
            return _mapper.Map<SliderVM>(await  _context.Sliders.FirstOrDefaultAsync(m=> m.Id == id));  
        }

		public async Task CreateAsync(SliderCreateVM slider)
		{
			string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

			string path = _env.GetFilePath("assets/img", fileName);

			var data = _mapper.Map<Slider>(slider);

			data.Image = fileName;

			await _context.AddAsync(data);

			await _context.SaveChangesAsync();

			await slider.Photo.SaveFileAsync(path);

		}

        public async Task EditAsync(SliderEditVM slider)
        {
            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == slider.Id);
            if (slider.Photo != null)
            {

                string oldPath = _env.GetFilePath("assets/img", slider.Image);

                string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

                string newPath = _env.GetFilePath("assets/img", fileName);
                dbSlider.Image = fileName;

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await slider.Photo.SaveFileAsync(newPath);

            }
            dbSlider.Head = slider.Head;
            dbSlider.Title = slider.Title;
            dbSlider.Description = slider.Description;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("img/hero/", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
