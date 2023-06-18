using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.MapperProfiles
{
    internal class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookModel>()
                  .ForMember(m => m.BookId, opt => opt
                  .MapFrom(e => e.BookId)
                  ).ReverseMap();
        }
    }
}
