using System;
namespace oneapp.Models
{
	public class CommentRequest
	{
        public Guid FeedId { get; set; }
        public string Description { get; set; }
    }
}

