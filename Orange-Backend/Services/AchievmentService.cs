using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class AchievmentService : IAchievmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public AchievmentService(AppDbContext context,
                                 IMapper mapper,
                                 IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<AchievmentVM> GetAllAsync()
        {
            return _mapper.Map<AchievmentVM>(await _context.Achievments.FirstOrDefaultAsync());
        }

		public async Task<AchievmentVM> GetByIdAsync(int id)
		{

			return _mapper.Map<AchievmentVM>(await _context.Achievments.FirstOrDefaultAsync(m => m.Id == id));
		}

        public async Task EditAsync(AchievmentEditVM request)
        {
            string oldPath = _env.GetFilePath("assets/img/about", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("assets/img/about", fileName);

            Achievment dbAdvert = await _context.Achievments.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbAdvert);

            dbAdvert.Image = fileName;

            _context.Achievments.Update(dbAdvert);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFileAsync(newPath);
        }
    }
}
