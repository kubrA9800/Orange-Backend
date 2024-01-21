using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ProductService(AppDbContext context,
                              IMapper mapper,
                              IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
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
                                                             .Include(m=>m.Brand)
                                                             .Include(m => m.Images)
                                                             .Skip((page * take) - take)
                                                             .Take(take)
                                                             .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }

        public async Task<Product> GetByIdWithIncludesAsync(int id)
        {
            Product data = await _context.Products.Include(m => m.Category)
                                                   .Include(m=>m.Brand)
                                                   .Include(m => m.Images)
                                                   .FirstOrDefaultAsync(m => m.Id == id);

            return data;
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


        public async Task<List<ProductVM>> GetProductsByCategoryAsync(int id, int page = 1, int take = 6)
        {
            List<ProductVM> model = new();

            var products = await _context.Products
                         .Where(m => m.CategoryId==id)
                         .Include(m => m.Images)
                         .Include(m => m.Category)
                         .Skip((page * take) - take)
                         .Take(take)
                         .ToListAsync();

           


                foreach (var product in products)
                {
                    model.Add(new ProductVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Images = product.Images,
                        
                    });
                }
            
            return model;

        }

        public async Task<int> GetCountByCategoryAsync(int[] id)
        {
            return await _context.Products.Where(m => id.Any(categoryId => categoryId == m.CategoryId))
                                           .Include(m=>m.Category)
                                           .Include(m => m.Images)
                                           .CountAsync();
        }


        public async Task DeleteAsync(int id)
        {
            Product dbproduct = await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);


            _context.Products.Remove(dbproduct);
            await _context.SaveChangesAsync();


            foreach (var photo in dbproduct.Images)
            {

                string path = _env.GetFilePath("assets/img/product", photo.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }


            }
        }



    }
}
