using Application.Common.Interfaces;
using Domain.Entities;
using Application.Common.Attributes;

namespace Application.Employees.Commands.CreateEmployee;
[RequireApiKey]
public record CreateEmployeeCommand(string FirstName, string LastName, DateTime HireDate, int DepartmentId, string Phone, string Address) : IRequest<int>
{
    
}

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.HireDate)
            .NotEmpty().WithMessage("Hire date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Hire date cannot be in the future.");

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Department ID must be greater than 0.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(100).WithMessage("Address must not exceed 100 characters.");
    }
}
 
public class CreateEmployeeCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            HireDate = request.HireDate,
            DepartmentId = request.DepartmentId,
            Phone = request.Phone,
            Address = request.Address
        };

        _context.Employees.Add(employee);


        await _context.SaveChangesAsync(cancellationToken);
        
        return employee.Id;
    }
}
