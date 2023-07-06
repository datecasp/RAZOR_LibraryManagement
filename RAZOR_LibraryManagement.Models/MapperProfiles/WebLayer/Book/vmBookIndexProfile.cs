using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.WebLayer.Book
{
    public class vmBookIndexProfile : Profile
    {
        public vmBookIndexProfile()
        {
            CreateMap<vmBookIndex, BookModel>()
                  .ForMember(m => m.BookId, opt => opt
                  .MapFrom(e => e.Id)
                  ).ReverseMap();
        }
    }
}
