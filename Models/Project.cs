using System.ComponentModel.DataAnnotations;

namespace COMP2139_ICE.Models;

public class Project
{
    
    /// <summary>
    /// unique identifier for the project
    /// </summary>
    public int ProjectId { get; set; }
    
    /// <summary>
    /// required field
    /// </summary>
    
    [Required]
    public required string Name { get; set; }
    
    public string? Description { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EDateTime { get; set; }
    
    public string? Status { get; set; }
    
    
    
    
}