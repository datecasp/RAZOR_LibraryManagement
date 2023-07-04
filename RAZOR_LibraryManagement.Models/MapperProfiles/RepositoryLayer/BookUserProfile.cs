using AutoMapper;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.RepositoryLayer
{
    public class BookUserProfile : Profile
    {
        public BookUserProfile()
        {
            //source mapping to destination
            CreateMap<BookUser, BookUserModel>()
                .ForMember(m => m.BookUserId, opt => opt
                .MapFrom(e => e.BookUserId)).ReverseMap();
        }
    }
}
