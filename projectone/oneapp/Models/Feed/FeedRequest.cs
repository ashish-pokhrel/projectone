using System;
using System.ComponentModel.DataAnnotations;

namespace oneapp.Models
{
	public class FeedRequest
	{
        [Required]
        public string Content { get; set; }
        public string Tags { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public List<IFormFile> FeedImages { get; set; }
    }
}

