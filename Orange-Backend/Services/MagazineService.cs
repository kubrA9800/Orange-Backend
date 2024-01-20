using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Magazine;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class MagazineService : IMagazineService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public MagazineService(AppDbContext context,
                                IMapper mapper,
                                IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<MagazineVM>> GetAllAsync()
        {
            return _mapper.Map<List<MagazineVM>>(await _context.Magazines.ToListAsync());
        }

		public async Task<MagazineVM> GetByIdAsync(int id)
		{
            return _mapper.Map<MagazineVM>(await _context.Magazines.FirstOrDefaultAsync(m => m.Id == id));
		}

        public async Task CreateAsync(MagazineCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
            string path = _env.GetFilePath("assets/img", fileName);

            var data = _mapper.Map<Magazine>(request);

            data.Image = fileName;

            await _context.Magazines.AddAsync(data);
            await _context.SaveChangesAsync();
            await request.Photo.SaveFileAsync(path);
        }

        public async Task EditAsync(MagazineEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {
                string oldPath = _env.GetFilePath("assets/img", request.Image);
                fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string newPath = _env.GetFilePath("assets/img", fileName);

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await request.Photo.SaveFileAsync(newPath);

            }
            else
            {
                fileName = request.Image;
            }

            Magazine dbMagazine = await _context.Magazines.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbMagazine);

            dbMagazine.Image = fileName;

            _context.Magazines.Update(dbMagazine);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Magazine magazine = await _context.Magazines.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Magazines.Remove(magazine);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("assets/img", magazine.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
    }
}
