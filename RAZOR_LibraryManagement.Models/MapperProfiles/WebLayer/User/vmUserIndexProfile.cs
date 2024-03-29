﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.WebLayer.User
{
    public class vmUserIndexProfile : Profile
    {
        public vmUserIndexProfile()
        {
            CreateMap<vmUserIndex, UserModel>()
                      .ForMember(m => m.UserId, opt => opt
                      .MapFrom(e => e.UserId)
                      ).ReverseMap();

        }
    }
}
