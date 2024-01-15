using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BannerService(AppDbContext context, 
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BannerVM> GetAllAsync()
        {
            return _mapper.Map<BannerVM>(await _context.Banners.FirstOrDefaultAsync());
        }
    }
}
