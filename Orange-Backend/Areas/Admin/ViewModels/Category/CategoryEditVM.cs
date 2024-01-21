using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Category
{
	public class CategoryEditVM
	{
		public int Id { get; set; }
        public string Image { get; set; }
		[Required]
		public string Name { get; set; }
        public IFormFile Photo { get; set; }
		public IList<SelectListItem> Brands { get; set; }
	}
}
