using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResolveDesk.Models;

namespace ResolveDesk.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketComment> TicketComments => Set<TicketComment>();
    public DbSet<TicketActivity> TicketActivities => Set<TicketActivity>();
    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);
        b.Entity<Department>().HasIndex(x => x.Name).IsUnique();
        b.Entity<Ticket>().HasIndex(x => x.Number).IsUnique();
        b.Entity<Ticket>().Property(x => x.RowVersion).IsConcurrencyToken();
        b.Entity<Ticket>().HasOne(x => x.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
        b.Entity<Ticket>().HasOne(x => x.Requester).WithMany().OnDelete(DeleteBehavior.Restrict);
        b.Entity<TicketComment>().HasOne(x => x.Ticket).WithMany(x => x.Comments).OnDelete(DeleteBehavior.Cascade);
        b.Entity<TicketActivity>().HasOne(x => x.Ticket).WithMany(x => x.Activities).OnDelete(DeleteBehavior.Cascade);
    }
}
