using System;
namespace oneapp.Entities
{
	public class Comment
	{
        public Guid Id { get; set; }
        public Guid FeedId { get; set; }
        public Guid CommentedBy { get; set; }
        public string Description { get; set; }
        public Guid? ParentCommentId { get; set; }
        public long TotalLikes { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}

