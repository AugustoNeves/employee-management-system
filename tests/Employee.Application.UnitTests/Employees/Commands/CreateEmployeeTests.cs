using Application.Common.Exceptions;
using Domain.Entities;
using Application.Departments.Commands.CreateDepartment;
using Application.Employees.Commands.CreateEmployee;

namespace Application.FunctionalTests.Employees.Commands;

using static Testing;
public class CreateEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldValidateDepartment()
    {
        var command = new CreateEmployeeCommand("John", "Does", DateTime.Now, 0, "+12345678901234", "1234 Elm St");
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateEmployee()
    {
        var departmentCommand = new CreateDepartmentCommand("IT");

        var departmentId = await SendAsync(departmentCommand);

        var emplyeeCommand = new CreateEmployeeCommand("John", "Does", DateTime.Now, departmentId, "+12345678901234", "1234 Elm St");
        
        var id = await SendAsync(emplyeeCommand);

        var employee = await FindAsync<Employee>(id);

        employee.Should().NotBeNull();
        employee.FirstName.Should().Be(emplyeeCommand.FirstName);
        employee.LastName.Should().Be(emplyeeCommand.LastName);
        employee.DepartmentId.Should().Be(departmentId);
    }
}
