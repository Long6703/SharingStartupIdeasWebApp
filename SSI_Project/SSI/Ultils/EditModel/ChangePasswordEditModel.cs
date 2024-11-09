using System.ComponentModel.DataAnnotations;

namespace SSI.Ultils.EditModel
{
    public class ChangePasswordEditModel
    {
        [Required(ErrorMessage = "Current Password is required.")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [RegularExpression(
            pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{4,12}$",
            ErrorMessage = "Password must be between 4 and 12 characters, containing at least 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character."
        )]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "NewPassword and confirmation do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
