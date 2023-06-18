using AutoMapper;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            //source mapping to destination
            CreateMap<Category, CategoryModel>()
                    .ForMember(m => m.CategoryId, opt => opt
                    .MapFrom(e => e.CategoryId)
                    ).ReverseMap();
        }
    }
}
