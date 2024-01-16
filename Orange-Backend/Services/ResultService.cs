using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Result;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ResultService : IResultService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ResultService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public async Task<ResultVM> GetAllAsync()
        {
            return _mapper.Map<ResultVM>(await _context.Results.FirstOrDefaultAsync());
        }
    }
}
