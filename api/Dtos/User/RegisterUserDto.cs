using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class RegisterUserDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Login must be 5 characters")]
        [MaxLength(20, ErrorMessage = "Login cannot be over 20 characters")]
        public string Login { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Password must be 5 characters")]
        [MaxLength(20, ErrorMessage = "Password cannot be over 20 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
