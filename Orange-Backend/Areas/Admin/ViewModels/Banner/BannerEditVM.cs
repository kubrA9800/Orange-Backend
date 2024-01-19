using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Banner
{
	public class BannerEditVM
	{
		public int Id { get; set; }
		[Required]
		public string Text { get; set; }
		public string Image { get; set; }
		public IFormFile Photo { get; set; }
	}
}
