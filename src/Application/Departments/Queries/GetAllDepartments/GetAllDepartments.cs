using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper.QueryableExtensions;

namespace Application.Departments.Queries.GetAllDepartments;

public record GetAllDepartmentsQuery : IRequest<IReadOnlyCollection<DepartmentDto>>;

public class GetAllDepartmentsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllDepartmentsQuery, IReadOnlyCollection<DepartmentDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AsNoTracking()
            .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);        
    }
}
