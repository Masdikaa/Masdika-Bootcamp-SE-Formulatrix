using Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Data;

public class HelpdeskDbContext : DbContext {

    public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder b) {
        b.Entity<User>(e => {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Name).IsRequired();
            e.Property(x => x.Email).IsRequired();
        });

        b.Entity<Ticket>(e => {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired();
            e.HasOne(x => x.Reporter)
                .WithMany(u => u.ReportedTickets)
                .HasForeignKey(x => x.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Assignee)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(x => x.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
