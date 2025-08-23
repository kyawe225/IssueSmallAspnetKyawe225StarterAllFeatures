
using IssuePj.Api.Entities;

namespace IssuePJ.Api.Command
{
    public class CreateIssueCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        // public string AssignedTo { get; set; }

        public CreateIssueCommand(string title, string description, string status, DateTime? dueDate, string assignedTo)
        {
            Title = title;
            Description = description;
            Status = status;
            DueDate = dueDate;
            // AssignedTo = assignedTo;
        }

        public CreateIssueCommand()
        {
            
        }

        public Issue toIssue()
        {
            return new Issue
            {
                Id = Guid.NewGuid().ToString(),
                Title = Title,
                Description = Description,
                Status = Status,
                CreatedAt = DateTime.UtcNow,
                DueDate = DueDate ?? DateTime.MinValue.ToUniversalTime(),
                // AssignedAt = DateTime.MinValue.ToUniversalTime()
            };
        }
    }
}