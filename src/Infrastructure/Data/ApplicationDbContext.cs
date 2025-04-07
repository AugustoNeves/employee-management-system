using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(e => e.FirstName)
            .HasMaxLength(200).IsRequired();


        modelBuilder.Entity<Department>()
            .Property(d => d.Name)
            .HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Department>()
             .HasMany(d => d.Employees)
             .WithOne(e => e.Department)
             .HasForeignKey(e => e.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);

    }
}