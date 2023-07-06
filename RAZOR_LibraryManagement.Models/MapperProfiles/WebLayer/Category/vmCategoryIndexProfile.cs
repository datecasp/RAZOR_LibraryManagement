using AutoMapper;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Models.MapperProfiles.WebLayer.Category
{
    public class vmCategoryIndexProfile : Profile
    {
        public vmCategoryIndexProfile()
        {
            CreateMap<vmCategoryIndex, CategoryModel>()
                  .ForMember(m => m.CategoryId, opt => opt
                  .MapFrom(e => e.CatId)
                  ).ReverseMap();
        }
    }
}
