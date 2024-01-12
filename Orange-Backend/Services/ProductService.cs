using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(AppDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductVM>> GetAllAsync()
        {
             return _mapper.Map<List<ProductVM>>(await _context.Products.Include(m=>m.BrandCategory.Category).Include(m=>m.Images).ToListAsync());
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products = await _context.Products.Include(m => m.BrandCategory.Category)
                                                             .Include(m => m.Images)
                                                             .Skip((page * take) - take)
                                                             .Take(take)
                                                             .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }
    }
}
