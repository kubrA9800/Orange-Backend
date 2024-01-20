using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BrandService(AppDbContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BrandVM>> GetAllAsync()
        {
            return _mapper.Map<List<BrandVM>>(await _context.Brands.ToListAsync());
        }

		public async Task<BrandVM> GetByNameAsync(string name)
		{

			return _mapper.Map<BrandVM>(await _context.Brands.FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower()));

		}

		
	}
}
