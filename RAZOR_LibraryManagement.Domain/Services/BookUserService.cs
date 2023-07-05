using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookUserService : IBookUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookUserModel> GetBookUserByIdService(int id)
        {
            var bu = await _unitOfWork.BookUserRepository.GetBookUserById(id);
            bu.Title = _unitOfWork.BookRepository.GetBookById(bu.BookId).Result.Title;
            bu.UserName = _unitOfWork.UserRepository.GetAllUsers().Result.Where(u => u.UserId == bu.UserId).Select(u => u.UserName).FirstOrDefault();
            return bu;
        }

        public async Task<IEnumerable<string>> GetBooksOfUser(int userId, bool? actualUser = null)
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

        public async Task<AllBooksAndUsersModel> GetBorrowableBooksAndUsersService()
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

        public async Task<bool> UserHasBooks(int userId)
        {
            return _unitOfWork.BookUserRepository.GetBooksOfUser(userId, true).Result.Any();
        }

        public async Task<IEnumerable<BookUserModel>> GetBookUserListService()
        {
            var res = _unitOfWork.BookUserRepository.GetAll().Result.Where(bu => bu.IsActualUser);
            var books = await _unitOfWork.BookRepository.GetAllBooks();
            var users = await _unitOfWork.UserRepository.GetAllUsers();
            foreach (var bu in res)
            {
                bu.Title = books.Where(b => b.BookId == bu.BookId).Select(b => b.Title).FirstOrDefault();
                bu.UserName = users.Where(u => u.UserId == bu.UserId).Select(u => u.UserName).FirstOrDefault();
            }
            return res;
        }

        public async Task<vmNotification> AddBookToUserService(int userId, int bookId)
        {
            var vmNotification = new vmNotification();

            var setting = _unitOfWork.AppSettingsRepository
                .GetAllSettings().Result
                .First(b => b.SettingParam.Equals("MaxNumOfBooks")
                );
            var borrowedBooksByUser = _unitOfWork.BookUserRepository
                .GetBooksOfUser(userId).Result
                .Where(bu => bu.IsActualUser)
                .Count();
            if (borrowedBooksByUser < setting.Value)
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
                    vmNotification.Type = Lang.Notification.NotificationType.Error;
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

        public async Task<vmNotification> ToggleBookUserIsActualUser(BookUserModel bu)
        {
            var vmNotification = new vmNotification();
            try
            {
                //var bu = await GetBookUserByIdService(id);
                //bu.IsActualUser = !bu.IsActualUser;
                
                var res = await _unitOfWork.BookUserRepository.UpdateBookUser(bu);
                if(bu != null)
                {
                    _unitOfWork.Save();
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "Operation success";
                }
                else
                {
                    vmNotification.Type = Lang.Notification.NotificationType.Error;
                    vmNotification.Message = "Hmmm something went wrong here....";
                }
            }
            catch (Exception e)
            {
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Big problem with database";
            }
            return vmNotification;
        }

    }
}
