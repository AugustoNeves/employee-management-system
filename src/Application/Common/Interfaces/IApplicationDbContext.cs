using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; }

        DbSet<Department> Departments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
