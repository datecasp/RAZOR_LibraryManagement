using RAZOR_LibraryManagement.Domain.Models;
using RAZOR_LibraryManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookService
    {
        Task<vmBookCreate> CreateBookService(vmBookCreate vmCreateBook);
        Task<IEnumerable<vmBookIndex>> GetAllBooksService();
        Task<vmBookDetails> GetBookByIdService(int id);
    }
}
