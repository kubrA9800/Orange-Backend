using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Info;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class InfoService : IInfoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public InfoService(AppDbContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<InfoVM> GetAllAsync()
        {
            return _mapper.Map<InfoVM>(await _context.Infos.FirstOrDefaultAsync());
        }

		public async Task<InfoVM> GetByIdAsync(int id)
		{
            return _mapper.Map<InfoVM>(await _context.Infos.FirstOrDefaultAsync(m => m.Id == id));
		}


        public async Task EditAsync(InfoEditVM request)
        {
            Info dbInfo = await _context.Infos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);

            _mapper.Map(request, dbInfo);
            _context.Infos.Update(dbInfo);
            await _context.SaveChangesAsync();

        }
    }
}
