namespace TaskManagementSystem.Core.Models.AccountModels
{
    using System.ComponentModel.DataAnnotations;
    using static TaskManagementSystem.Core.Constants.ApplicationUserConstants;
    public class ApplicationUserRegisterModel
    {
        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}