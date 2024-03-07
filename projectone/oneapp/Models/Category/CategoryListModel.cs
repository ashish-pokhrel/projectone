namespace oneapp.Models
{
	public class CategoryViewModel
	{
        public int Count { get; set; }

        public IEnumerable<CategoryResponse> List { get; set; }
    }
}

