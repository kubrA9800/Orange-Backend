using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Subscribe
{
    public class SubscribeCreateVM
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
