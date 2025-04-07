using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand(string Name) : IRequest<int>;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
    }
}

public class CreateDepartmentCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department
        {
            Name = request.Name
        };

        _context.Departments.Add(department);
        
        await _context.SaveChangesAsync(cancellationToken);

        return department.Id;
    }
}
