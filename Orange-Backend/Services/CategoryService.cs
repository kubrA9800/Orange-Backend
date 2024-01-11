using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(AppDbContext context, 
                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CategoryVM>> GetAllAsync()
        {
            return _mapper.Map<List<CategoryVM>>(await _context.Categories.Include(m=>m.Products).ToListAsync());
        }
    }
}
