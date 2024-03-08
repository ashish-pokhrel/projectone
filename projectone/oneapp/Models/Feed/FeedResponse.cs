using Newtonsoft.Json;

namespace oneapp.Models
{
	public class FeedResponse
	{
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public long TotalLikes { get; set; }
        public long TotalShares { get; set; }
        public string ProfileImage { get; set; }
        public IEnumerable<string> Images { get; set; }

        [JsonIgnore]
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeHelperModel CreatedOnDetails
        {
            get
            {
                return new DateTimeHelperModel { FullDateTime = CreatedOn };
            }
        }

        public string CreatedByName { get; set; }
    }
}

