namespace oneapp.Models
{
	public class CategoryRequest
	{
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public FormFile Image { get; set; }
        public bool ShowInMenu { get; set; }
    }
}

