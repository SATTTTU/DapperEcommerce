namespace EcommerceApp.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Enums.UserRole Role { get; set; }= Enums.UserRole.Customer;
    }
}
