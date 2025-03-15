using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2139_ICE.Areas.ProjectManagement.Models;

public class ProjectTask
{
    [Key]
    public int Id { get; set; }  // Unique identifier for the task

    [Display(Name = "Task Title")]
    [Required]
    [StringLength(100, ErrorMessage = "Task Title must be between 3 and 100 characters!")]
    public required string Title { get; set; }

    [Required]
    [Display(Name = "Task Description")]
    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = "Task Description must be between 3 and 500 characters!")]
    public required string Description { get; set; }

    // Foreign key
    [Display(Name = "Parent Project ID")]
    public int ProjectId { get; set; }

    
    public Project? Project { get; set; }
    
}
