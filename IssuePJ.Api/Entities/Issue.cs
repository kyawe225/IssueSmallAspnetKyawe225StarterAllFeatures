


using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IssuePj.Api.Entities;

public class Issue
{
    [Key]
    public string Id { set; get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { set; get; }
    public DateTime CreatedAt { set; get; }
    // public DateTime AssignedAt { set; get; }
    public string Status { set; get; }
}
