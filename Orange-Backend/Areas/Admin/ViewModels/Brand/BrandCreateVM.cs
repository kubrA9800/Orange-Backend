using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Brand
{
	public class BrandCreateVM
	{
		[Required]
		public IFormFile Photo { get; set; }
		[Required]
		public string Name { get; set; }
		public IList<SelectListItem> Categories { get; set; }
	}
}
