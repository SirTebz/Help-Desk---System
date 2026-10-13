using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResolveDesk.Models;
namespace ResolveDesk.Data;
public static class SeedData {
 public static async Task InitializeAsync(IServiceProvider sp) { var db=sp.GetRequiredService<ApplicationDbContext>(); await db.Database.EnsureCreatedAsync(); var roles=sp.GetRequiredService<RoleManager<IdentityRole>>(); foreach(var r in new[]{"EndUser","Agent","DepartmentAdmin","SuperAdmin"}) if(!await roles.RoleExistsAsync(r)) await roles.CreateAsync(new IdentityRole(r)); var users=sp.GetRequiredService<UserManager<ApplicationUser>>();
  async Task<ApplicationUser> User(string email,string name,string role){var u=await users.FindByEmailAsync(email); if(u is null){u=new ApplicationUser{UserName=email,Email=email,EmailConfirmed=true,DisplayName=name}; await users.CreateAsync(u,"Demo!12345"); await users.AddToRoleAsync(u,role);} return u;}
  var admin=await User("admin@resolvedesk.local","Sarah Mitchell","SuperAdmin"); var agent=await User("agent@resolvedesk.local","Priya Sharma","Agent"); var customer=await User("user@resolvedesk.local","James Wilson","EndUser");
  foreach(var n in new[]{"IT Support","Software Support","Network Operations","Infrastructure","Human Resources","Finance","Facilities","Security and Access Control"}) if(!await db.Departments.AnyAsync(x=>x.Name==n)) db.Departments.Add(new Department{Name=n,Description=$"{n} service queue"}); await db.SaveChangesAsync();
  if(!await db.Tickets.AnyAsync()){var it=await db.Departments.FirstAsync(); var now=DateTime.UtcNow; db.Tickets.AddRange(new Ticket{Number="RD-000001",Subject="Cannot access VPN from home office",Description="VPN sign-in fails after entering my credentials.",DepartmentId=it.Id,RequesterId=customer.Id,AssignedToId=agent.Id,Priority=TicketPriority.Critical,Status=TicketStatus.InProgress,Category="Network",CreatedUtc=now.AddHours(-2),UpdatedUtc=now,ResolutionDueUtc=now.AddHours(2)},new Ticket{Number="RD-000002",Subject="Email client not synchronizing",Description="Outlook is not receiving new messages.",DepartmentId=it.Id,RequesterId=customer.Id,Priority=TicketPriority.High,Status=TicketStatus.Open,Category="Software",CreatedUtc=now.AddHours(-3),UpdatedUtc=now,ResolutionDueUtc=now.AddHours(5)}); await db.SaveChangesAsync();}
 }
}
