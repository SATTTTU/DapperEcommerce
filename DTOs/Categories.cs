namespace EcommerceApp.DTOs
{
    public class CategoryDTOs
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ParentId { get; set; }
    }

    public class CategoryResponseDtos
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ParentId { get; set; }
    }
}
