using ResolveDesk.Models;
using ResolveDesk.Services;
namespace ResolveDesk.Tests;
public class TicketWorkflowServiceTests
{
    private readonly TicketWorkflowService _service = new();
    [Fact]
    public void Allows_the_normal_staff_workflow() => Assert.True(_service.CanTransition(TicketStatus.InProgress, TicketStatus.Resolved, true));
    [Fact]
    public void Rejects_invalid_or_non_staff_transition()
    {
        Assert.False(_service.CanTransition(TicketStatus.New, TicketStatus.Closed, true));
        Assert.False(_service.CanTransition(TicketStatus.Open, TicketStatus.Resolved, false));
    }
    [Theory]
    [InlineData(TicketPriority.Critical, 4)]
    [InlineData(TicketPriority.High, 8)]
    [InlineData(TicketPriority.Medium, 24)]
    [InlineData(TicketPriority.Low, 72)]
    public void Calculates_priority_resolution_targets(TicketPriority priority, int hours)
    {
        var created = new DateTime(2026, 10, 13, 8, 0, 0, DateTimeKind.Utc);
        Assert.Equal(created.AddHours(hours), _service.ResolutionDeadline(priority, created));
    }
}
