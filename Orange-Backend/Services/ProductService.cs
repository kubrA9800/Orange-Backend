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
             return _mapper.Map<List<ProductVM>>(await _context.Products.Include(m=>m.Category).Include(m=>m.Images).ToListAsync());
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Category)
                                                             .Include(m => m.Images)
                                                             .Skip((page * take) - take)
                                                             .Take(take)
                                                             .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }

        public async Task<ProductVM> GetByIdWithIncludesAsync(int id)
        {
            Product data = await _context.Products.Include(m => m.Category)
                                                   .Include(m=>m.Brand)
                                                   .Include(m => m.Images)
                                                   .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<ProductVM>(data);
        }

        public async Task<Product> GetProductDatasModalAsync(int id)
        {
            var data = await _context.Products
          .Include(m => m.Images)
          .Include(m=>m.Category)
          .Include(m => m.Brand)
          .FirstOrDefaultAsync(m => m.Id == id);

          return data;
        }


        public async Task<List<ProductVM>> GetProductsByCategoryAsync(int? id, int page = 1, int take = 6)
        {
            List<ProductVM> model = new();

            var products = await _context.Products
                 .Where(m => m.CategoryId == id)
                 .Include(m => m.Images)
                 .Include(m => m.Category)
                 .Skip((page * take) - take)
                 .Take(take)
                 .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Images = item.Images,
                });
            }
            return model;

        }

        public async Task<int> GetCountByCategoryAsync(int id)
        {
            return await _context.Products.Include(m=>m.CategoryId == id)
                                           .Include(m=>m.Category)
                                           .Include(m => m.Images)
                                           .CountAsync();
        }

        
    }
}
