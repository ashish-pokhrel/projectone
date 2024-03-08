namespace oneapp.Models
{
	public class CategoryRequest
	{
        public string CategoryName { get; set; }
        public IFormFile Image { get; set; }
        public bool ShowInMenu { get; set; }
    }
}

