namespace oneapp.Models
{
    public class ImageHubModel
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

