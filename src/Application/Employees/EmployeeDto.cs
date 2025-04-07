using Application.DTOs;
using Domain.Entities;

namespace Application.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime HireDate { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }

        public DepartmentDto Department { get; init; } = null!;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Employee, EmployeeDto>();
                CreateMap<EmployeeDto, Employee>();
            }
        }
    }

    
}
