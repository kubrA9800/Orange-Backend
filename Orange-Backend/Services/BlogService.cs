using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Blog;
using Orange_Backend.Data;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BlogService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BlogVM>> GetAllAsync()
        {
            return _mapper.Map<List<BlogVM>>(await _context.Blogs.ToListAsync());
        }
    }
}
