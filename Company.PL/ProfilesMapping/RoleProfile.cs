using AutoMapper;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Company.PL.ProfilesMapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDto,IdentityRole>().ReverseMap();
        }
    }
}
