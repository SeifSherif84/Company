using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Company.PL.ProfilesMapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
