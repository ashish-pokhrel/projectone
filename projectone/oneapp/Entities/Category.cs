namespace oneapp.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool ShowInMenu { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}

