namespace TaskManagementSystem.Core.Models.AccountModels
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUserLoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}