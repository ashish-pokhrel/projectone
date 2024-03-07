namespace oneapp.Models
{
	public class CategoryResponse
	{
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool ShowInMenu { get; set; }
    }
}

