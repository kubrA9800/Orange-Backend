using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Magazine;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class MagazineService : IMagazineService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public MagazineService(AppDbContext context,
                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<MagazineVM>> GetAllAsync()
        {
            return _mapper.Map<List<MagazineVM>>(await _context.Magazines.ToListAsync());
        }
    }
}
