using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Contact
{
	public class ContactContentEditVM
	{
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
		[Required]
		public string Head { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public string Info { get; set; }
	}
}
