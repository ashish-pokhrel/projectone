using System.ComponentModel.DataAnnotations;

namespace oneapp.Models
{
	public class CategoryRequest
	{
        [Required]
        public string CategoryName { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public bool ShowInMenu { get; set; }
    }
}

