using Microsoft.AspNetCore.Http;


namespace BusinessLogic.Models.AccountModels
{
    public class RegisterUserModel
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public IFormFile? AvatarFile { get; set; }
    }
}
