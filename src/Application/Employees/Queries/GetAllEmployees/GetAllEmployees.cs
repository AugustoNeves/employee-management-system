using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Application.Common.Attributes;

namespace Application.Employees.Queries.GetAllEmployees;

[RequireApiKey]
public record GetAllEmployeesQuery : IRequest<IReadOnlyCollection<EmployeeDto>>;

public class GetAllEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllEmployeesQuery, IReadOnlyCollection<EmployeeDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .AsNoTracking()
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
