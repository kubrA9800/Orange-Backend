using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Models;

namespace Orange_Backend.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactContent> GetAllAsync();
        Task CreateAsync(ContactMessageCreateVM contact);

    }
}
