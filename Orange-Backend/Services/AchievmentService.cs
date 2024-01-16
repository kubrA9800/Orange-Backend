using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class AchievmentService : IAchievmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AchievmentService(AppDbContext context,
                                 IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AchievmentVM> GetAllAsync()
        {
            return _mapper.Map<AchievmentVM>(await _context.Achievments.FirstOrDefaultAsync());
        }
    }
}
