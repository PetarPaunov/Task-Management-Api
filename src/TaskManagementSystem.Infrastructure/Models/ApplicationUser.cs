namespace TaskManagementSystem.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;

    using static TaskManagementSystem.Infrastructure.Constants.ApplicationUserConstants;

    public class ApplicationUser
    {
        public ApplicationUser()
        {
            this.Tasks = new HashSet<UserTask>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(UsernameMaxLength)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public byte[] Password { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public IEnumerable<UserTask> Tasks { get; set; }
    }
}