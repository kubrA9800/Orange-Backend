using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Achievment;
using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Areas.Admin.ViewModels.Review;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ReviewService(AppDbContext context,
                                 IMapper mapper,
                                 IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<ReviewVM>> GetAllAsync()
        {
            return _mapper.Map<List<ReviewVM>>(await _context.Reviews.ToListAsync());
        }

		public async Task<ReviewVM> GetByIdAsync(int id)
		{
			return _mapper.Map<ReviewVM>(await  _context.Reviews.FirstOrDefaultAsync(m => m.Id == id));
		}

		public async Task DeleteAsync(int id)
		{
			Review review = await _context.Reviews.Where(m => m.Id == id).FirstOrDefaultAsync();
			_context.Reviews.Remove(review);
			await _context.SaveChangesAsync();
		}


        public async Task<List<ReviewVM>> GetReviewsByProductAsync(int id)
        {
            List<ReviewVM> model = new();

            var reviews = await _context.Reviews
                         .Where(m => m.ProductId == id)
                         .ToListAsync();




            foreach (var review in reviews)
            {
                model.Add(new ReviewVM
                {
                    Id = review.Id,
                    Name = review.Name,
                    Email = review.Email,
                    Title = review.Title,
                    Message=review.Message,
                    CreatedDate =review.CreatedDate,

                });
            }

            return model;

        }
    }
}
