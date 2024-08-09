
namespace BusinessLogic.DTOs
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
