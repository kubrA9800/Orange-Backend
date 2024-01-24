using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
