using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;
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

        public async Task<AllBooksAndUsersModel> GetAllBooksAndUsersService()
        {
            var users = (await _unitOfWork.UserRepository.GetAllUsers()).Where(u => u.IsActive).ToList();
            var books = (await _unitOfWork.BookRepository.GetAllBooks()).Where(b => b.IsBorrowable).ToList();
            var borrowedBooksIds = (await _unitOfWork.BookUserRepository.GetBorrowedBooks()).ToList();
            var avaliableBooks = new List<BookModel>();
            foreach (var book in books)
            {
                if (!borrowedBooksIds.Contains(book.BookId))
                {
                    avaliableBooks.Add(books.Where(b => b.BookId == book.BookId).FirstOrDefault());
                }
            }
            var result = new AllBooksAndUsersModel
            {
                Books = avaliableBooks,
                Users = users
            };
            return result;
        }

        public async Task<vmNotification> AddBookToUserService(int userId, int bookId)
        {
            var vmNotification = new vmNotification();

            var setting = _unitOfWork.AppSettingsRepository
                .GetAllSettings().Result
                .First(b => b.SettingParam.Equals("MaxNumberOfBooks")
                );
            var borrowedBooksByUser = _unitOfWork.BookUserRepository
                .GetBooksOfUser(userId).Result
                .Where(bu => bu.IsActualUser)
                .Count();
            if(borrowedBooksByUser < setting.Value)
            {
                var bookUser = new BookUserModel
                {
                    BookId = bookId,
                    UserId = userId,
                    IsActualUser = true,
                    BorrowDate = DateTime.Now
                };
                var resultUser = _unitOfWork.BookUserRepository.AddBookToUser(bookUser, setting.Value).Result;
                if (resultUser != null)
                {
                    _unitOfWork.Save();
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "Book added to user successfully";
                }
                else
                {
                    vmNotification.Type = Lang.Notification.NotificationType.Info;
                    vmNotification.Message = "Hmmm something went wrong here....";
                }
            }
            else
            {
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "User has maximun number of books borrowed.";
            }

            return vmNotification;
        }

    }
}
