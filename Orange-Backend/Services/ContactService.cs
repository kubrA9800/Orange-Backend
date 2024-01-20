using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Areas.Admin.ViewModels.Banner;
using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Data;
using Orange_Backend.Helpers.Extensions;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;

namespace Orange_Backend.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
        public ContactService(AppDbContext context,
                              IMapper mapper,
							  IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
			_env = env;
        }
        public async Task<ContactContent> GetAllAsync()
        {
            return await _context.ContactContents.FirstOrDefaultAsync();
        }

		public async Task<ContactContent> GetByIdAsync(int id)
		{
			return await _context.ContactContents.FirstOrDefaultAsync(m=>m.Id==id);
		}

		public async Task CreateAsync(ContactMessageCreateVM contact)
        {
            var data = _mapper.Map<ContactMessage>(contact);
            await _context.ContactMessages.AddAsync(data);
            await _context.SaveChangesAsync();

        }

		public async Task EditAsync(ContactContentEditVM request)
		{
			string oldPath = _env.GetFilePath("assets/img", request.Image);

			string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

			string newPath = _env.GetFilePath("assets/img", fileName);

			Banner dbBanner = await _context.Banners.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


			_mapper.Map(request, dbBanner);

			dbBanner.Image = fileName;

			_context.Banners.Update(dbBanner);
			await _context.SaveChangesAsync();



			if (File.Exists(oldPath))
			{
				File.Delete(oldPath);
			}

			await request.Photo.SaveFileAsync(newPath);
		}

        public async Task<List<ContactMessage>> GetAllMessagesAsync()
        {
            return await _context.ContactMessages.ToListAsync();
        }

		public async Task<ContactMessage> GetMessageByIdAsync(int id)
		{
			return await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task DeleteAsync(int id)
		{
			ContactMessage dbContactMessage = await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
			_context.ContactMessages.Remove(dbContactMessage);
			await _context.SaveChangesAsync();
		}
	}
}
