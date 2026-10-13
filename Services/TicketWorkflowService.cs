using ResolveDesk.Models;
namespace ResolveDesk.Services;
public interface ITicketWorkflowService { bool CanTransition(TicketStatus from, TicketStatus to, bool staff); DateTime ResolutionDeadline(TicketPriority priority, DateTime created); }
public class TicketWorkflowService : ITicketWorkflowService {
 public bool CanTransition(TicketStatus from, TicketStatus to, bool staff) => staff && (from, to) switch { (TicketStatus.New, TicketStatus.Open or TicketStatus.Assigned or TicketStatus.Escalated) => true, (TicketStatus.Open or TicketStatus.Assigned, TicketStatus.InProgress or TicketStatus.Pending or TicketStatus.WaitingForCustomer or TicketStatus.Escalated) => true, (TicketStatus.InProgress, TicketStatus.Pending or TicketStatus.WaitingForCustomer or TicketStatus.Resolved or TicketStatus.Escalated) => true, (TicketStatus.Pending or TicketStatus.WaitingForCustomer or TicketStatus.Escalated, TicketStatus.InProgress or TicketStatus.Resolved) => true, (TicketStatus.Resolved, TicketStatus.Closed or TicketStatus.InProgress) => true, _ => false };
 public DateTime ResolutionDeadline(TicketPriority p, DateTime created) => created.AddHours(p switch { TicketPriority.Critical => 4, TicketPriority.High => 8, TicketPriority.Medium => 24, _ => 72 });
}
