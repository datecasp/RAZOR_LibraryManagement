using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookUserRepository
    {
        Task<IEnumerable<BookUserModel>> GetAll();
        Task<BookUserModel> GetBookUserById(int id);
        Task<IEnumerable<BookUserModel>> GetBooksOfUser(int userId, bool? actualUser = null);
        Task<BookUserModel> AddBookToUser(BookUserModel bookUserModel, int maxBooks);
        Task<IEnumerable<int>> GetBorrowerUsers();
        Task<IEnumerable<int>> GetBorrowedBooks();
        Task<IEnumerable<int>> GetNotBorrowedBooks();
        Task<BookUserModel> UpdateBookUser(BookUserModel bu);
    }
}
