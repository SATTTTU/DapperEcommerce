namespace EcommerceApp.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public int ParentId { get; set; }
    }
}
