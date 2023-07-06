using AutoMapper;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.RepositoryLayer
{
    public class BookProfile : Profile
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
