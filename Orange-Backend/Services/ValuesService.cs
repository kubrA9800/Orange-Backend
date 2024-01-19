using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Values;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ValuesService : IValuesService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ValuesService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ValuesVM> GetAllAsync()
        {
            return _mapper.Map<ValuesVM>(await _context.Values.FirstOrDefaultAsync());
        }

		public async Task<ValuesVM> GetByIdAsync(int id)
		{
			return _mapper.Map<ValuesVM>(await _context.Values.FirstOrDefaultAsync(m=>m.Id==id));
		}

		public async Task EditAsync(ValuesEditVM request)
		{
			Values dbValue = await _context.Values.FirstOrDefaultAsync(m => m.Id == request.Id);

			_mapper.Map(request, dbValue);

			_context.Values.Update(dbValue);

			await _context.SaveChangesAsync();
		}
	}
}
