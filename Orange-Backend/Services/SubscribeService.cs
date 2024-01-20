using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Subscribe;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class SubscribeService:ISubscribeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SubscribeService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(SubscribeCreateVM subscribe)
        {
            //var existEmail = await _context.Subscribes.FirstOrDefaultAsync(m => m.Email == subscribe.Email);

            var data = _mapper.Map<Subscribe>(subscribe);

            await _context.Subscribes.AddAsync(data);

            await _context.SaveChangesAsync();
        }

        public async Task<List<SubscribeVM>> GetAllAsync()
        {
            List<Subscribe> subscribes = await _context.Subscribes.ToListAsync();

            return _mapper.Map<List<SubscribeVM>>(subscribes);
        }

        public async Task DeleteAsync(int id)
        {
            Subscribe subscribe = await _context.Subscribes.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Subscribes.Remove(subscribe);
            await _context.SaveChangesAsync();
        }
    }
}
