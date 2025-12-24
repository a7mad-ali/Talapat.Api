using System.ComponentModel.DataAnnotations;

namespace Talapat.Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;
        [Required]
        [EmailAddress]

        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$",
    ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = null!;
    }
}
