namespace oneapp.Models
{
	public class CommentListModel
	{
        public int Count { get; set; }

        public IEnumerable<CommentResponse> List { get; set; }
    }
}

