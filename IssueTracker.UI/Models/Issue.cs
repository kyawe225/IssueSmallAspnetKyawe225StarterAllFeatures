using System.ComponentModel.DataAnnotations;

namespace IssueTracker.UI.Models;

public class Issue
{
    public string Id { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;
    
    public DateTime DueDate { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = string.Empty;
}