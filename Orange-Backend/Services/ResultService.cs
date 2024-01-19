using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Result;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ResultService : IResultService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
        public ResultService(AppDbContext context,
                             IMapper mapper,
							 IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper; 
			_env = env;
        }
        public async Task<ResultVM> GetAllAsync()
        {
            return _mapper.Map<ResultVM>(await _context.Results.FirstOrDefaultAsync());
        }

		public async Task<ResultVM> GetByIdAsync(int id)
		{
			return _mapper.Map<ResultVM>(await _context.Results.FirstOrDefaultAsync(m=>m.Id==id));
		}

		public async Task EditAsync(ResultEditVM request)
		{
			string oldPath = _env.GetFilePath("assets/img/about", request.Image);

			string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

			string newPath = _env.GetFilePath("assets/img/about", fileName);

			Result dbResult = await _context.Results.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


			_mapper.Map(request, dbResult);

			dbResult.Image = fileName;

			_context.Results.Update(dbResult);
			await _context.SaveChangesAsync();



			if (File.Exists(oldPath))
			{
				File.Delete(oldPath);
			}

			await request.Photo.SaveFileAsync(newPath);
		}
	}
}
