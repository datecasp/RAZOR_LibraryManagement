using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.RepositoryLayer
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                   .ForMember(m => m.UserId, opt => opt
                   .MapFrom(e => e.UserId)
                   ).ReverseMap();
        }
    }
}
