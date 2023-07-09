using AutoMapper;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Models.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //  Entity <-> Model
            CreateMap<AppSettingsEntity, AppSettingsModel>()
                .ReverseMap();
            CreateMap<Book, BookModel>()
                .ReverseMap();
            CreateMap<BookUser, BookUserModel>()
                .ReverseMap();
            CreateMap<Category, CategoryModel>()
                .ReverseMap();
            CreateMap<User, UserModel>()
                .ReverseMap();

            // Model <-> ViewModel
            CreateMap<vmBookCreate, BookModel>()
                .ReverseMap();
            CreateMap<vmBookDetails, BookModel>()
                .ReverseMap();
            CreateMap<vmBookIndex, BookModel>()
                .ReverseMap();
            CreateMap<vmBookUser, BookUserModel>()
                .ReverseMap();
            CreateMap<vmCategoryIndex, CategoryModel>()
                .ReverseMap();
            CreateMap<vmUserIndex, UserModel>()
                .ReverseMap();
            CreateMap<vmUserEdit, UserModel>()
                .ReverseMap();
            CreateMap<vmUserCreate, UserModel>()
                .ReverseMap();
        }
    }
}
