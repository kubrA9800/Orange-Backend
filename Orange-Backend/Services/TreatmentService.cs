using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Treatment;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public TreatmentService(AppDbContext context,
                                 IMapper mapper,
                                 IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<TreatmentVM> GetAllAsync()
        {
            return _mapper.Map<TreatmentVM>(await _context.Treatments.FirstOrDefaultAsync());
        }

		public async Task<TreatmentVM> GetByIdAsync(int id)
		{
			return _mapper.Map<TreatmentVM>(await _context.Treatments.FirstOrDefaultAsync(x => x.Id == id));
		}


        public async Task EditAsync(TreatmentEditVM request)
        {
            string oldPath = _env.GetFilePath("assets/img", request.Image1);
            string oldPath2 = _env.GetFilePath("assets/img", request.Image2);

            string fileName = $"{Guid.NewGuid()} - {request.Photo1.FileName}";
            string fileName2 = $"{Guid.NewGuid()} - {request.Photo2.FileName}";

            string newPath = _env.GetFilePath("assets/img", fileName);
            string newPath2 = _env.GetFilePath("assets/img", fileName2);

            Treatment dbTreatment = await _context.Treatments.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbTreatment);

            dbTreatment.Image1 = fileName;
            dbTreatment.Image2 = fileName2;

            _context.Treatments.Update(dbTreatment);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            if (File.Exists(oldPath2))
            {
                File.Delete(oldPath2);
            }


            await request.Photo1.SaveFileAsync(newPath);
            await request.Photo2.SaveFileAsync(newPath2);
        }
    }
}
