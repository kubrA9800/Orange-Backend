using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
        public BannerService(AppDbContext context, 
                             IMapper mapper,
							 IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
			_env = env;
        }
        public async Task<BannerVM> GetAllAsync()
        {
            return _mapper.Map<BannerVM>(await _context.Banners.FirstOrDefaultAsync());
        }

		public async Task<BannerVM> GetByIdAsync(int id)
		{
			return _mapper.Map<BannerVM>(await _context.Banners.FirstOrDefaultAsync(m => m.Id == id));

		}

		public async Task EditAsync(BannerEditVM request)
		{
			string oldPath = _env.GetFilePath("assets/img/about", request.Image);

			string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

			string newPath = _env.GetFilePath("assets/img/about", fileName);

			Banner dbBanner = await _context.Banners.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


			_mapper.Map(request, dbBanner);

			dbBanner.Image = fileName;

			_context.Banners.Update(dbBanner);
			await _context.SaveChangesAsync();



			if (File.Exists(oldPath))
			{
				File.Delete(oldPath);
			}

			await request.Photo.SaveFileAsync(newPath);
		}
	}
}
