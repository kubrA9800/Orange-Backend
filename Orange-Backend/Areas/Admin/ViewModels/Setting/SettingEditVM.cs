using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Setting
{
    public class SettingEditVM
    {
        public int Id { get; set; }
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        public IFormFile ImageValue { get; set; }
    }
}
