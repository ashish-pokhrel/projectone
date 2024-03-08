using System;
namespace oneapp.Models
{
	public class FeedListModel
	{
        public int Count { get; set; }

        public IEnumerable<FeedResponse> List { get; set; }
    }
}

