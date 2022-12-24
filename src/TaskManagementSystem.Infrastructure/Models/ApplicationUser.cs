namespace TaskManagementSystem.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}