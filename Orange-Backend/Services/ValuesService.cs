using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Values;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ValuesService : IValuesService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ValuesService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ValuesVM> GetAllAsync()
        {
            return _mapper.Map<ValuesVM>(await _context.Values.FirstOrDefaultAsync());
        }
    }
}
