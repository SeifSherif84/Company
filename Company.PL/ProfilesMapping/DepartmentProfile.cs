using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;

namespace Company.PL.ProfilesMapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentDto,Department>().ReverseMap();
        }
    }
}
