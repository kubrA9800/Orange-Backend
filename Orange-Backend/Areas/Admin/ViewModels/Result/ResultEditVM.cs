using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Result
{
	public class ResultEditVM
	{
        public int Id { get; set; }
        public string Image { get; set; }
		[Required]
		public string Head { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Position { get; set; }
        public IFormFile Photo { get; set; }
    }
}
