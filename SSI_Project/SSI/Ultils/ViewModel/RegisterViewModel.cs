using static SSI.Ultils.Enums.Enums;
using System.ComponentModel.DataAnnotations;
using SSI.Ultils.Enums;

namespace SSI.Ultils.ViewModel
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(50, ErrorMessage = "Display name cannot exceed 50 characters.")]
        public string? DisplayName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(
            pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{4,12}$",
            ErrorMessage = "Password must be between 4 and 12 characters, containing at least 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character."
        )]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match")]
        public string ConfirmPassword { get; set; } = null!;

        public string Role { get; set; } = null!;

        public StatusEnum Status { get; set; } = StatusEnum.Active;
    }
}
