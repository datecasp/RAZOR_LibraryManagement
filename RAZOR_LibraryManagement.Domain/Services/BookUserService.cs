using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookUserService : IBookUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<string>> GetBooksOfUser(int userId)
        {
            var booksIdList = _unitOfWork.BookUserRepository.GetBooksOfUser(userId).Result.ToList();
            var booksTitleList = new List<string>();
            foreach (var bookId in booksIdList)
            {
                var title = _unitOfWork.BookRepository.GetBookById(bookId.BookId).Result.Title;
                booksTitleList.Add(title);
            }
            return booksTitleList;
        }
        public async Task<vmUserDetails> GetVmUserDetails(int userId)
        {
            var vmUser = new vmUserDetails();
            var user = _unitOfWork.UserRepository.GetAllUsers().Result
                .Where(u => u.UserId == userId).FirstOrDefault();
            vmUser.UserId = user.UserId;
            vmUser.UserName = user.UserName;
            vmUser.PhoneNumber = user.PhoneNumber;
            vmUser.Email = user.Email;
            vmUser.IsActive = user.IsActive;
            vmUser.ActualBooks = new List<string>();
            vmUser.HistoricBooks = new List<string>();
            var bookUserList = _unitOfWork.BookUserRepository.GetBooksOfUser(userId).Result.ToList();
            foreach (var bookUser in bookUserList)
            {
                var book = _unitOfWork.BookRepository.GetBookById(bookUser.BookId).Result;
                if (bookUser.IsActualUser)
                {
                    vmUser.ActualBooks.Add(book.Title);
                }
                else
                {
                    vmUser.HistoricBooks.Add(book.Title);
                }
            }
            return vmUser;
        }
    }
}
