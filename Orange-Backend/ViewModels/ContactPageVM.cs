using Orange_Backend.Areas.Admin.ViewModels.Contact;
using Orange_Backend.Models;

namespace Orange_Backend.ViewModels
{
    public class ContactPageVM
    {
        public ContactContent ContactContent { get; set; }
        public ContactMessageCreateVM NewContact { get; set; }
    }
}
