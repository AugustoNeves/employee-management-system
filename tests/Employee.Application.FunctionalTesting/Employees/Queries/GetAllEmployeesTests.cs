using Application.Employees.Queries.GetAllEmployees;
using Domain.Entities;

namespace Application.FunctionalTests.Employees.Queries;

using static Testing;

[TestFixture]
public class GetAllEmployeesTests : BaseTestFixture
{

    [Test]
    public async Task ShouldReturnsAllEmployees()
    {
        var query = new GetAllEmployeesQuery();

        await AddAsync(new Employee { FirstName = "John", LastName = "Doe", Department = new Department() {Name = "HR" } });
        await AddAsync(new Employee { FirstName = "Peter", LastName = "Parker", Department = new Department() { Name = "Sales" } });

        var result = await SendAsync(query);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
    }
}
