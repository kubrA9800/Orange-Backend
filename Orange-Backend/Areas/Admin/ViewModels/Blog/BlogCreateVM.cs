using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Blog
{
	public class BlogCreateVM
	{
		[Required]
		public IFormFile Photo { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Head { get; set; }
		[Required]
		public string Text { get; set; }

	}
}
