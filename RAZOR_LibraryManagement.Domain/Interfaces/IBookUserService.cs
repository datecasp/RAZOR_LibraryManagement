using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookUserService
    {
        Task<IEnumerable<string>> GetBooksOfUser(int userId);
        Task<vmUserDetails> GetVmUserDetails(int userId);
    }
}
