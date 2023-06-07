using RAZOR_LibraryManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksService();
    }
}
