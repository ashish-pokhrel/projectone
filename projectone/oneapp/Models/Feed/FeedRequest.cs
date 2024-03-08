using System;
namespace oneapp.Models
{
	public class FeedRequest
	{
        public string Content { get; set; }
        public string Tags { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public List<IFormFile> FeedImages { get; set; }
    }
}

