namespace TaskManagementSystem.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TaskManagementSystem.Infrastructure.Emuns;

    using static TaskManagementSystem.Infrastructure.Constants.UserTaskConstants;

    public class UserTask
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public State State { get; set; }

        [Required]
        public Importance Importance { get; set; }

        public Guid ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public bool IsFinished { get; set; }
    }
}