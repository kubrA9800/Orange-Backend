using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Slider
{
	public class SliderCreateVM
	{
		[Required]
		public IFormFile Photo { get; set; }
		[Required]
		public string Head { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
}
