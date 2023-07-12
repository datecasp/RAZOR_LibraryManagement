using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;
using System.Text.RegularExpressions;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookModel"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public async Task<vmNotification> CreateUpdateBookService(BookModel bookModel, bool isUpdate)
        {
            var bookRepository = _unitOfWork.GetRepository<Book>();
            var vmNotification = new vmNotification();
            bookModel.UrlHandle = FormatUrl(bookModel.Title);
            try
            {
                var bookResult = new BookModel();
                if (isUpdate)
                {
                    bookResult = bookRepository.Update<BookModel>(bookModel);
                }
                else
                {
                    bookResult = bookRepository.Insert<BookModel>(bookModel);
                }
                _unitOfWork.Save();
                if (bookResult != null)
                {
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "Operation done successfully";
                    return vmNotification;
                }
            }
            catch (Exception ex)
            {
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Exception thrown! " + ex.Message;
                return vmNotification;
            }
            vmNotification.Type = Lang.Notification.NotificationType.Error;
            vmNotification.Message = "Hmmm something went wrong here....";
            return vmNotification;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<vmBookIndex>> GetAllBooksService()
        {
            var booksList = new List<vmBookIndex>();
            try
            {
                var repo = _unitOfWork.GetRepository<Book>(); 
                var bookUserRepo = _unitOfWork.GetRepository<BookUser>();
                booksList = (await repo.GetAllProfiled<vmBookIndex>()).ToList();
                foreach(var book in booksList)
                {
                    if(bookUserRepo.Get<BookUserModel>(bu => (bu.BookId == book.BookId && bu.IsActualUser)).Any())
                    {
                        book.IsBorrowed = true;
                    }
                    else
                    {
                        book.IsBorrowed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var vmNotification = new vmNotification();
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Hmmm something went wrong here....";
                // TODO Send this to controller and loger
            }
            return booksList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BookModel> GetBookByIdService(int id)
        {
            var book = new BookModel();
            try
            {
                var repo = _unitOfWork.GetRepository<Book>();
                book = repo.GetByIdProfiled<BookModel>(id).Result;
            }
            catch (Exception ex)
            {

            }
            return book;
        }


        #region Private methods

        /// <summary>
        /// Replace white spaces with dashes for readability in url 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string FormatUrl(string url)
        {
            var result = Regex.Replace(url, " ", "-").ToLower();
            return result;
        }

        #endregion
    }
}
