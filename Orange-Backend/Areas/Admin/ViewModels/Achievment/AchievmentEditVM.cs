using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Achievment
{
    public class AchievmentEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Head { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Results { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
