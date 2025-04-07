using Application.Common.Interfaces;
using Application.Common.Attributes;

namespace Application.Employees.Commands.DeleteEmployee;
[RequireApiKey]
public record DeleteEmployeeCommand(int Id) : IRequest<bool>
{
}

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
    }
}

public class DeleteEmployeeCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.FindAsync([request.Id, cancellationToken], cancellationToken: cancellationToken);

        if (entity == null)
            return false;

        _context.Employees.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
