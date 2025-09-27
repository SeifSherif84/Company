using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;

namespace Company.PL.ProfilesMapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>()/*.ReverseMap()*/;

            CreateMap<Employee, EmployeeDto>()
                     .ForMember(D => D.DepartmentName, M => M.MapFrom(S => S.Department.Name));

        }
    }
}
