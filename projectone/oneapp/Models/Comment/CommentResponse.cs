using System;
using System.Text.Json.Serialization;

namespace oneapp.Models
{
	public class CommentResponse
	{
        public Guid Id { get; set; }
        public Guid FeedId { get; set; }
        public string Description { get; set; }
        public Guid? ParentCommentId { get; set; }
        public long TotalLikes { get; set; }
        public string ProfileImage { get; set; }
        public string CommentedByName { get; set; }

        [JsonIgnore]
        public DateTimeOffset UpdatedOn { get; set; }
        public DateTimeHelperModel CommentedOnDetails
        {
            get
            {
                return new DateTimeHelperModel { FullDateTime = UpdatedOn };
            }
        }
    }
}

