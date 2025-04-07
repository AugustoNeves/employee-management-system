using Application.Common.Interfaces;
using Application.Common.Attributes;

namespace Application.Employees.Commands.UpdateEmployee;
[RequireApiKey]
public record UpdateEmployeeCommand(int Id, string FirstName, string LastName, DateTime HireDate, int DepartmentId, string Phone, string Address) : IRequest<bool>
{
}

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
    }
}

public class UpdateEmployeeCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync([request.Id, cancellationToken], cancellationToken: cancellationToken);

        if (employee == null) return false;

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.HireDate = request.HireDate;
        employee.DepartmentId = request.DepartmentId;
        employee.Phone = request.Phone;
        employee.Address = request.Address;

        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
