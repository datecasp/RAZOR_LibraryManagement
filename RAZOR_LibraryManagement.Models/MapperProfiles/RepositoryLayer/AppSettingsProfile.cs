using AutoMapper;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.RepositoryLayer
{
    public class AppSettingsProfile : Profile
    {
        public AppSettingsProfile()
        {
            //source mapping to destination
            CreateMap<AppSettingsEntity, AppSettingsModel>()
                .ForMember(m => m.AppSettingModelId, opt => opt
                .MapFrom(e => e.AppSettingEntityId)
                ).ReverseMap();
        }
    }
}
