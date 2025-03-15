using System.ComponentModel.DataAnnotations;

namespace COMP2139_ICE.Areas.ProjectManagement.Models;

public class Project
{
    
    /// <summary>
    /// unique identifier for the project
    /// </summary>
    public int ProjectId { get; set; }
    
    /// <summary>
    /// required field
    /// </summary>
    [Display(Name = "Project Name")]
    [Required]
    [StringLength(100, ErrorMessage = "Project Name must be between 3 and 100 characters!")]
    
    public required string Name { get; set; }
    
    [Display(Name = "Project Description")]
    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = "Project Description must be between 3 and 500 characters!")]
    public string? Description { get; set; }
    
    [Display(Name = "Project Start Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime StartDate { get; set; }
    
    
    [Display(Name = "Project End Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? EndDate  { get; set; }
    
    [Display(Name = "Project Status")]
    public string? Status { get; set; }
    
    public List<ProjectTask>? Tasks { get; set; } = new();
   
}