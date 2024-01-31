using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Brand;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public BrandService(AppDbContext context,
                            IMapper mapper,
                            IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<BrandVM>> GetAllAsync()
        {
            var data = _mapper.Map<List<BrandVM>>(await _context.Brands.Include(m => m.Products)
                                    .Include(m => m.BrandCategories)
                                    .ThenInclude(m => m.Category)
                                    .ToListAsync());

            return data;
        }

        public async Task<BrandVM> GetByNameAsync(string name)
		{

			return _mapper.Map<BrandVM>(await _context.Brands.FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower()));

		}


		public List<SelectListItem> GetAllSelectedAsync()
		{
			return _context.Brands.Select(m => new SelectListItem()
			{
				Text = m.Name,
				Value = m.Id.ToString(),

			}).ToList();
		}

        public async Task<BrandVM> GetByIdAsync(int id)
        {
            return _mapper.Map<BrandVM>(await _context.Brands.Include(m => m.BrandCategories)
                                    .ThenInclude(m => m.Category).FirstOrDefaultAsync(m => m.Id == id));
        }


        public async Task CreateAsync(BrandCreateVM brand)
        {

            string fileName = $"{Guid.NewGuid()}-{brand.Photo.FileName}";

            string path = _env.GetFilePath("assets/img", fileName);

            await brand.Photo.SaveFileAsync(path);

            var selectedCategories = brand.Categories.Where(m => m.Selected).Select(m => m.Value).ToList();


            var dbBrand = new Brand()
            {
                Name = brand.Name,
                Image = fileName
            };

            foreach (var item in selectedCategories)
            {
                dbBrand.BrandCategories.Add(new BrandCategory()
                {
                    CategoryId = int.Parse(item)
                });
            }

            await _context.Brands.AddAsync(dbBrand);
            await _context.SaveChangesAsync();
        }


        public async Task EditAsync(BrandEditVM brand)
        {
            string fileName = $"{Guid.NewGuid()} - {brand.Photo.FileName}";

            string path = _env.GetFilePath("assets/img", fileName);

            await brand.Photo.SaveFileAsync(path);

            var brandById = await _context.Brands.Include(m => m.BrandCategories).FirstOrDefaultAsync(m => m.Id == brand.Id);

            var existingIds = brandById.BrandCategories.Select(m => m.CategoryId).ToList();

            var selectedIds = brand.Categories.Where(m => m.Selected).Select(m => m.Value).Select(int.Parse).ToList();


            var toAdd = selectedIds.Where(id => !existingIds.Contains(id)).ToList();

            var toRemove = existingIds.Where(id => !selectedIds.Contains((int)id)).ToList();

            brandById.BrandCategories = brandById.BrandCategories.Where(m => !toRemove.Contains(m.CategoryId)).ToList();

            foreach (var item in toAdd)
            {
                brandById.BrandCategories.Add(new BrandCategory
                {
                    CategoryId = item
                });
            }


            brand.Image = fileName;

            _mapper.Map(brand, brandById);

            _context.Brands.Update(brandById);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            Brand dbBrand = await _context.Brands.Where(m => m.Id == id)
                                                .Include(m => m.BrandCategories)
                                                .ThenInclude(m => m.Category)
                                                .FirstOrDefaultAsync();


            _context.Brands.Remove(dbBrand);
            await _context.SaveChangesAsync();
            string path = _env.GetFilePath("assets/images", dbBrand.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
