using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Services;
using RAZOR_LibraryManagement.Infra.Repositories;
using RAZOR_LibraryManagement.Infra.UnitOfWork;

namespace RAZOR_LibraryManagement.Web.Configuration
{
    public static class DependencyInjection
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>(); 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookUserRepository, BookUserRepository>();
            services.AddScoped<IBookUserService, BookUserService>();
            services.AddScoped<IImageRepository, ImageRepositoryCloudinary>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAppSettingsService, AppSettingsService>();
            services.AddScoped<IAppSettingsRepository, AppSettingsRepository>();

            return services;
        }
    }

}
