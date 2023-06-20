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
        Task<IEnumerable<BookUserModel>> GetBooksOfUser(int userId);
    }
}
