using MeetingManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace MeetingManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Agenda> AgendaItems { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Templates> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.Project)
                .WithMany(p => p.Meetings)
                .HasForeignKey(m => m.ProjectId);

            modelBuilder.Entity<Agenda>()
                .HasOne(a => a.Meeting)
                .WithMany(m => m.AgendaItems)
                .HasForeignKey(a => a.MeetingId);

            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Meeting)
                .WithMany(m => m.Tasks)
                .HasForeignKey(t => t.MeetingId);
        }
    }
}
