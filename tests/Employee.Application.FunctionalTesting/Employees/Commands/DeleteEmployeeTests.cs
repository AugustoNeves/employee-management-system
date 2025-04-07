using Domain.Entities;
using Application.Departments.Commands.CreateDepartment;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.DeleteEmployee;

namespace Application.FunctionalTests.Employees.Commands;

using static Testing;

public class DeleteEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldValidateExistingEmployee()
    {
        var command = new DeleteEmployeeCommand(20);
        bool result = await SendAsync(command);
        result.Should().BeFalse();
    }

    [Test]
    public async Task ShouldDeleteEmployee()
    {
        var departmentCommand = new CreateDepartmentCommand("IT");
        var departmentId = await SendAsync(departmentCommand);
        var emplyeeCommand = new CreateEmployeeCommand("John", "Does", DateTime.Now, departmentId, "+12345678901234", "1234 Elm St");
        var id = await SendAsync(emplyeeCommand);
        var deleteEmployeeCommand = new DeleteEmployeeCommand(id);
        var result = await SendAsync(deleteEmployeeCommand);
        var employee = await FindAsync<Employee>(id);
        result.Should().BeTrue();
        employee.Should().BeNull();
    }
}
