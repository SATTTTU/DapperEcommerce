namespace EcommerceApp.DTOs
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

    public class RegisterResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }

}
