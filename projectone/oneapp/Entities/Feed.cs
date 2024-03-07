using System;

namespace oneapp.Entities
{
    public class Feed
    {
        public string Content { get; set; }
        public string Tags { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public Guid LocationId { get; set; }
        public long TotalLikes { get; set; }
        public long TotalShares { get; set; }
        public string ProfileImage { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}

