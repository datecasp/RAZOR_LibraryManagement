using RAZOR_LibraryManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<vmCategoryIndex>> GetAllCategoriesService();
    }
}
