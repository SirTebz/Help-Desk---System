using System.ComponentModel.DataAnnotations;
using ResolveDesk.Models;
namespace ResolveDesk.ViewModels;
public class TicketCreateVm { [Required, StringLength(180)] public string Subject { get; set; } = ""; [Required, StringLength(6000)] public string Description { get; set; } = ""; public int DepartmentId { get; set; } public TicketPriority Priority { get; set; } = TicketPriority.Medium; [StringLength(80)] public string Category { get; set; } = "General"; }
public class DashboardVm { public int Open { get; set; } public int Mine { get; set; } public int AtRisk { get; set; } public int ResolvedToday { get; set; } public int[] VolumeSeries { get; set; } = []; public int[] ResolutionSeries { get; set; } = []; public string[] DepartmentLabels { get; set; } = []; public int[] DepartmentLoad { get; set; } = []; public int[] PrioritySla { get; set; } = []; public List<Ticket> Recent { get; set; } = []; }
