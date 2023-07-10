using System.Text.RegularExpressions;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Lang.Category;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<vmNotification> CreateUpdateBookService(BookModel bookModel, bool isUpdate)
        {
            var vmNotification = new vmNotification();
            bookModel.UrlHandle = FormatUrl(bookModel.Title);
            try
            {
                var bookResult = new BookModel();
                if (isUpdate)
                {
                    bookResult = await _unitOfWork.BookRepository.UpdateBook(bookModel);
                }
                else
                {
                    bookResult = await _unitOfWork.BookRepository.CreateBook(bookModel);
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

        public async Task<IEnumerable<BookModel>> GetAllBooksService()
        {
            var booksList = new List<BookModel>();
            try
            {
                var repo = _unitOfWork.GetRepository<Book>();
                booksList = repo.GetAllProfiled<BookModel>().Result.ToList();
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

        //Replace white spaces with dashes for readability in url 
        private static string FormatUrl(string url)
        {
            var result = Regex.Replace(url, " ", "-").ToLower();
            return result;
        }

        #endregion
    }
}
