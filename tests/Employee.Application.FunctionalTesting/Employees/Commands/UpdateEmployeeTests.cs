using Domain.Entities;
using Application.Departments.Commands.CreateDepartment;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.UpdateEmployee;


namespace Application.FunctionalTests.Employees.Commands;

using static Testing;
public class UpdateEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldValidateExistingEmployee()
    {
        var command = new UpdateEmployeeCommand(20, "John", "Does", DateTime.Now, 0, "+12345678901234", "1234 Elm St");
        bool result = await SendAsync(command);
        result.Should().BeFalse();
    }
    [Test]
    public async Task ShouldUpdateEmployee()
    {
        var departmentCommand = new CreateDepartmentCommand("IT");
        var departmentId = await SendAsync(departmentCommand);
        var emplyeeCommand = new CreateEmployeeCommand("John", "Does", DateTime.Now, departmentId, "+12345678901234", "1234 Elm St");

        var id = await SendAsync(emplyeeCommand);

        var updateEmployeeCommand = new UpdateEmployeeCommand(id, "Johns", "Doe", DateTime.Now, departmentId, "+12345678901234", "12345 Elm St");
        var result = await SendAsync(updateEmployeeCommand);

        var employee = await FindAsync<Employee>(id);
        result.Should().BeTrue();   
        employee.Should().NotBeNull();
        employee.FirstName.Should().Be(updateEmployeeCommand.FirstName);
        employee.LastName.Should().Be(updateEmployeeCommand.LastName);
        employee.Address.Should().Be(updateEmployeeCommand.Address);
        employee.DepartmentId.Should().Be(departmentId);
    }
}
