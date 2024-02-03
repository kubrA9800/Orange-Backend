using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Treatment
{
	public class TreatmentEditVM
	{
		public int Id { get; set; }
		public string Image1 { get; set; }
		public IFormFile Photo1 { get; set; }
		public string Image2 { get; set; }
		public IFormFile Photo2 { get; set; }
		[Required]
		public string Head { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
}
