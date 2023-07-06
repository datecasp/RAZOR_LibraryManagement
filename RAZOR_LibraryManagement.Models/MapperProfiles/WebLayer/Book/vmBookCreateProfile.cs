using AutoMapper;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.WebLayer.Book
{
    public class vmBookCreateProfile : Profile
    {
        public vmBookCreateProfile()
        {
            CreateMap<vmBookCreate, BookModel>()
                  .ForMember(m => m.BookId, opt => opt
                  .MapFrom(e => e.BookId)
                  ).ReverseMap();
        }
    }
}
