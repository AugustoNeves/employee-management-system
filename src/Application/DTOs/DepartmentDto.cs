using Domain.Entities;

namespace Application.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Department, DepartmentDto>();                
            }
        }
    }
}
