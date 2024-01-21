using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Category
{
	public class CategoryCreateVM
	{
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Name { get; set; }
        public  IList<SelectListItem> Brands { get; set; }
    }
}
