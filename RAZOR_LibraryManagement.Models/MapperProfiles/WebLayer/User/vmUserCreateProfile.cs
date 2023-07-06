using AutoMapper;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.WebLayer.User
{
    public class vmUserCreateProfile : Profile
    {
        public vmUserCreateProfile()
        {
            CreateMap<vmUserCreate, UserModel>()
                  .ForMember(m => m.UserId, opt => opt
                  .MapFrom(e => e.UserId)
                  ).ReverseMap();
        }
    }
}
