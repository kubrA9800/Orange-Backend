using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Orange_Backend.Areas.Admin.ViewModels.Magazine
{
	public class MagazineCreateVM
	{
		[Required]
		public string Text { get; set; }
		[Required]
        public IFormFile Photo { get; set; }
    }
}
