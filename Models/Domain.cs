using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ResolveDesk.Models;
public class ApplicationUser : IdentityUser { [MaxLength(120)] public string DisplayName { get; set; } = ""; }
public class Department { public int Id { get; set; } [Required, MaxLength(80)] public string Name { get; set; } = ""; [MaxLength(300)] public string Description { get; set; } = ""; public bool IsActive { get; set; } = true; }
public enum TicketStatus { New, Open, Assigned, InProgress, Pending, WaitingForCustomer, Escalated, Resolved, Closed }
public enum TicketPriority { Low, Medium, High, Critical }
public class Ticket {
 public int Id { get; set; } [MaxLength(20)] public string Number { get; set; } = ""; [Required, MaxLength(180)] public string Subject { get; set; } = ""; [Required, MaxLength(6000)] public string Description { get; set; } = "";
 public TicketStatus Status { get; set; } = TicketStatus.New; public TicketPriority Priority { get; set; } = TicketPriority.Medium; [MaxLength(80)] public string Category { get; set; } = "General";
 public int DepartmentId { get; set; } public Department Department { get; set; } = null!; [Required] public string RequesterId { get; set; } = ""; public ApplicationUser Requester { get; set; } = null!; public string? AssignedToId { get; set; }
 public DateTime CreatedUtc { get; set; } = DateTime.UtcNow; public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow; public DateTime? FirstResponseUtc { get; set; } public DateTime ResolutionDueUtc { get; set; } public DateTime? ResolvedUtc { get; set; } public int EscalationLevel { get; set; } public Guid RowVersion { get; set; } = Guid.NewGuid();
 public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>(); public ICollection<TicketActivity> Activities { get; set; } = new List<TicketActivity>();
}
public class TicketComment { public int Id { get; set; } public int TicketId { get; set; } public Ticket Ticket { get; set; } = null!; [Required] public string AuthorId { get; set; } = ""; [Required, MaxLength(4000)] public string Body { get; set; } = ""; public bool IsInternal { get; set; } public DateTime CreatedUtc { get; set; } = DateTime.UtcNow; }
public class TicketActivity { public int Id { get; set; } public int TicketId { get; set; } public Ticket Ticket { get; set; } = null!; [MaxLength(120)] public string Action { get; set; } = ""; [MaxLength(600)] public string Detail { get; set; } = ""; public string? ActorId { get; set; } public DateTime CreatedUtc { get; set; } = DateTime.UtcNow; }
