using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactContent> GetAllAsync();
        Task<ContactContent> GetByIdAsync(int id);
        Task EditAsync(ContactContentEditVM request);
        Task CreateAsync(ContactMessageCreateVM contact);
        Task<List<ContactMessage>> GetAllMessagesAsync();
        Task<ContactMessage> GetMessageByIdAsync(int id);
		Task DeleteAsync(int id);


	}
}
