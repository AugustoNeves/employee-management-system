using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Application.Common.Attributes;

namespace Application.Employees.Queries.GetEmployeeById;

[RequireApiKey]
public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto?>
{
}

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Employee ID must be greater than 0.");
        RuleFor(x => x.Id).NotNull().WithMessage("Employee ID cannot be null.");
    }
}

public class GetEmployeeByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(x => x.Department)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}
