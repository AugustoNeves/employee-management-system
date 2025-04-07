using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Entities;


namespace Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Employees.Any())
        {
            _context.Employees.Add(new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                HireDate = DateTime.Now.AddMonths(-14),
                Phone = "123-456-7890",
                Address = "1234 Elm St",
                Department = new() { Name = "HR" }
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Mary",
                LastName = "Jane",
                HireDate = DateTime.Now.AddDays(-15),
                Phone = "123-456-3211",
                Address = "4321 Big St",
                Department = new() { Name = "Engineering" }
            });

            await _context.SaveChangesAsync();
        }
    }
}
