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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BookUserModel> GetBookUserByIdService(int id)
        {
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var bookUserRepo = _unitOfWork.GetRepository<BookUser>();
            var userRepo = _unitOfWork.GetRepository<User>();
            var bu = await bookUserRepo.GetByIdProfiled<BookUserModel>(id);
            bu.Title = bookRepo.GetByIdProfiled<BookModel>(bu.BookId).Result.Title;
            bu.UserName = userRepo.Get<UserModel>(u => u.UserId == bu.UserId).Select(u => u.UserName).FirstOrDefault();
            return bu;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="actualUser"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetBooksOfUser(int userId, bool? actualUser = null)
        {
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var bookUserRepo = _unitOfWork.GetRepository<BookUser>();
            var booksIdList = bookUserRepo.Get<BookUserModel>(bu => (bu.UserId == userId && bu.IsActualUser));
            var booksTitleList = new List<string>();
            foreach (var bookId in booksIdList)
            {
                var title = bookRepo.GetByIdProfiled<BookModel>(bookId.BookId).Result.Title;
                booksTitleList.Add(title);
            }
            return booksTitleList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<vmUserDetails> GetVmUserDetails(int userId)
        {
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var bookUserRepo = _unitOfWork.GetRepository<BookUser>();
            var userRepo = _unitOfWork.GetRepository<User>();

            var user = userRepo.Get<UserModel>(u => u.UserId == userId).FirstOrDefault();
            var vmUser = _mapper.Map<vmUserDetails>(user);
            
            vmUser.ActualBooks = new List<string>();
            vmUser.HistoricBooks = new List<string>();
            var bookUserList = bookUserRepo.Get<BookUserModel>(bu => bu.UserId == userId);
            foreach (var bookUser in bookUserList)
            {
                var book = bookRepo.Get<BookModel>(b => b.BookId == bookUser.BookId).FirstOrDefault();
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<AllBooksAndUsersModel> GetBorrowableBooksAndUsersService()
        {
            var userRepo = _unitOfWork.GetRepository<User>();
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var bookUsersRepo = _unitOfWork.GetRepository<BookUser>();
            var users = userRepo.Get<UserModel>(u => u.IsActive);
            var books = bookRepo.Get<BookModel>(b => b.IsBorrowable);
            var borrowedBooksIds = bookUsersRepo.Get<BookUserModel>(bu => bu.IsActualUser).Select(bu => bu.BookId);
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
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UserHasBooks(int userId)
        {
            var bookUserRepo = _unitOfWork.GetRepository<BookUser>();
            return bookUserRepo.Get<BookUserModel>(bu => (bu.UserId == userId && bu.IsActualUser)).Any();
            //return _unitOfWork.BookUserRepository.GetBooksOfUser(userId, true).Result.Any();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BookUserModel>> GetBookUserListService()
        {
            var userRepo = _unitOfWork.GetRepository<User>();
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var bookUsersRepo = _unitOfWork.GetRepository<BookUser>();

            var res = bookUsersRepo.Get<BookUserModel>(bu => bu.IsActualUser);
            var books = await bookRepo.GetAllProfiled<BookModel>();
            var users = await userRepo.GetAllProfiled<UserModel>();
            foreach (var bu in res)
            {
                bu.Title = books.Where(b => b.BookId == bu.BookId).Select(b => b.Title).FirstOrDefault();
                bu.UserName = users.Where(u => u.UserId == bu.UserId).Select(u => u.UserName).FirstOrDefault();
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<vmNotification> AddBookToUserService(int userId, int bookId)
        {
            var vmNotification = new vmNotification();

            var settingsRepo = _unitOfWork.GetRepository<AppSettingsEntity>();
            var bookUserRepository = _unitOfWork.GetRepository<BookUser>();
            var setting = settingsRepo.Get<AppSettingsModel>(s => s.SettingParam.Equals("MaxNumberOfBooks")).FirstOrDefault();
            var borrowedBooksByUser = bookUserRepository.Get<BookUserModel>(bu => bu.UserId == userId && bu.IsActualUser == true).Count();

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bu"></param>
        /// <returns></returns>
        public async Task<vmNotification> ToggleBookUserIsActualUser(BookUserModel bu)
        {
            var vmNotification = new vmNotification();
            try
            {
                var bookUserRepository = _unitOfWork.GetRepository<BookUser>();
                bookUserRepository.Update<BookUserModel>(bu);

                _unitOfWork.Save();
                vmNotification.Type = Lang.Notification.NotificationType.Success;
                vmNotification.Message = "Operation success";
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
