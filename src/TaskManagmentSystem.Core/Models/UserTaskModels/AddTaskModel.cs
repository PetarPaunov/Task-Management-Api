namespace TaskManagementSystem.Core.Models.UserTaskModels
{
    using System.ComponentModel.DataAnnotations;

    using static TaskManagementSystem.Core.Constants.UserTaskConstants;
    public class AddTaskModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public string Importance { get; set; } = null!;
    }
}