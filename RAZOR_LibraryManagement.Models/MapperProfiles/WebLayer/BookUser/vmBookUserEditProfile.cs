using AutoMapper;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.WebLayer.BookUser
{
    internal class vmBookUserEditProfile : Profile
    {
        public vmBookUserEditProfile()
        {
            CreateMap<vmBookUser, BookUserModel>()
                  .ForMember(m => m.BookUserId, opt => opt
                  .MapFrom(e => e.BookUserId)
                  ).ReverseMap();
        }
    }
}
