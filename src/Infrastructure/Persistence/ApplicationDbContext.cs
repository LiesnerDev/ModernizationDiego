using Microsoft.EntityFrameworkCore;
using Employee.Domain.Entities;

namespace Employee.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EmployeeRecord> EmployeeRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeRecord>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);
                entity.Property(e => e.EmployeeName)
                      .IsRequired()
                      .HasMaxLength(20);
                entity.Property(e => e.EmployeeAge)
                      .IsRequired();
                entity.Property(e => e.EmployeeAddress)
                      .IsRequired()
                      .HasMaxLength(30);
            });
        }
    }
}