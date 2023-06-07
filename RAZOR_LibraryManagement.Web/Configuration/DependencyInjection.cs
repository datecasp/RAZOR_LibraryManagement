using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Services;
using RAZOR_LibraryManagement.Infra.Repositories;

namespace RAZOR_LibraryManagement.Web.Configuration
{
    public static class DependencyInjection
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
          
            return services;
        }
    }

}
