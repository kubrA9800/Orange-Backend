using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Areas.Admin.ViewModels.Category;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Orange_Backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
        public CategoryService(AppDbContext context, 
                                IMapper mapper,	
								IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
			_env = env;
        }
        public async Task<List<CategoryVM>> GetAllAsync()
        {
            return _mapper.Map<List<CategoryVM>>(await _context.Categories.Include(m=>m.Products).Include(m => m.BrandCategories).ThenInclude(m => m.Brand).ToListAsync());
        }

        public async Task<CategoryVM> GetByIdAsync(int id)
        {
            return _mapper.Map<CategoryVM>(await _context.Categories.FirstOrDefaultAsync(m => m.Id == id));
        }

		public async Task<CategoryVM> GetByNameWithoutTrackingAsync(string name)
		{
			Category category = await _context.Categories.Where(m => m.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefaultAsync();

			return _mapper.Map<CategoryVM>(category);
		}

		public async Task CreateAsync(CategoryCreateVM category)
		{

			string fileName = $"{Guid.NewGuid()}-{category.Photo.FileName}";

			string path = _env.GetFilePath("assets/img", fileName);

			await category.Photo.SaveFileAsync(path);

			var selectedBrands = category.Brands.Where(m => m.Selected).Select(m => m.Value).ToList();


			var dbCategory = new Category()
			{
				Name = category.Name,
				Image=fileName
		};

			foreach (var item in selectedBrands)
			{
				dbCategory.BrandCategories.Add(new BrandCategory()
				{
					BrandId = int.Parse(item)
				});
			}


			
			await _context.Categories.AddAsync(dbCategory);
			await _context.SaveChangesAsync();
		}
	}
}
