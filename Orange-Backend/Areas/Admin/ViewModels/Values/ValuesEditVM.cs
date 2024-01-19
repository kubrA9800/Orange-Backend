using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Values
{
	public class ValuesEditVM
	{
		public int Id { get; set; }
		[Required]
		public string Head { get; set; }
		[Required]
		public string Text { get; set; }
	}
}
